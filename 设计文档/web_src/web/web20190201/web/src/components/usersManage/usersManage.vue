<template>
  <div>
    <div class="craeteTop">
      <p>用户管理</p>
    </div>
    <div class="unltlistBot">
      <!--input 搜索框-->
      <div class="logHeader">
        <span>单位名称</span>
        <Select v-model="UnitClassification" style="width:200px">
          <Option
            v-for="item in UnitClassificationOption"
            :value="item.value"
            :key="item.value"
          >{{ item.label }}</Option>
        </Select>

        <!-- <span>职位级别</span>
        <Select class="ivSelect" v-model="PositionClassification" style="width:200px">
          <Option v-for="item in PositionClassificationOption" :value="item.value" :key="item.value">{{ item.label }}</Option>
        </Select>-->

        <!-- <span>性别</span>
        <Select class="ivSelect" v-model="sex" style="width:200px">
          <Option v-for="item in sexOption" :value="item.value" :key="item.value">{{ item.label }}</Option>
        </Select>-->
        <!-- <span>关键字</span>
        <Input class="unltlistInput" v-model="value1" placeholder="搜索关键字..." style="width:200px;" />-->
        <Button class="primary" type="primary" @click="queryUserList">搜索</Button>
        <div class="ivButton">
          <Button class="ivButtons" @click="xjbtn">新建账户</Button>
          <Modal width="570" title="账户设置" v-model="modal10" class-name="vertical-center-modal">
            <div style="text-align:center">
              <Form ref="formInline">
                <div class="ps">
                  <span>账户ID</span>
                  <Input
                    calss="iv-input"
                    placeholder="请输入..."
                    clearable
                    style="width: 240px"
                    v-model="id"
                  />
                </div>
                <div class="ps" v-if="isEdit">
                  <span>密码</span>
                  <Input
                    calss="iv-input"
                    type="password"
                    placeholder="请输入..."
                    clearable
                    style="width: 240px"
                    v-model="pwd"
                  />
                </div>
                <div class="ps" v-if="isEdit">
                  <span>确认密码</span>
                  <Input
                    calss="iv-input"
                    type="password"
                    placeholder="请输入..."
                    clearable
                    style="width: 240px"
                    v-model="pwdagain"
                  />
                </div>
                <div class="ps" style="height:50px;">
                  <span>单位分类</span>
                  <RadioGroup v-model="area" on-change="handClickUnitPart">
                    <template v-for="(cell, cellIndex) in OrganTypeList">
                      <span :key="cellIndex" @click="handClickUnitPart(cell.OrganTypeID)">
                        <Radio :label="cell.OrganTypeID" :value="cell.OrganTypeID">
                          <span>{{cell.OrganTypeName}}</span>
                        </Radio>
                      </span>
                    </template>
                  </RadioGroup>

                  <Tabs :animated="false" v-if="false">
                    <template v-for="(item,index) in TownToDo">
                      <TabPane :key="index" :label="item.CategoryName" :name="item.CategoryName">
                        <RadioGroup v-model="area" on-change="handClickUnitPart">
                          <template v-for="(cell, cellIndex) in item.OrganTypeList">
                            <span :key="cellIndex" @click="handClickUnitPart(cell.OrganTypeID)">
                              <!-- <Radio v-if="cell.disabled==true" disabled :label="cell.OrganTypeID" :value="cell.OrganTypeID">
                                  <span>{{cell.OrganTypeName}}</span>
                                </Radio>
                                <Radio v-else :label="cell.OrganTypeID" :value="cell.OrganTypeID">
                                  <span>{{cell.OrganTypeName}}</span>
                              </Radio>-->
                              <Radio :label="cell.OrganTypeID" :value="cell.OrganTypeID">
                                <span>{{cell.OrganTypeName}}</span>
                              </Radio>
                            </span>
                          </template>
                        </RadioGroup>
                      </TabPane>
                    </template>
                  </Tabs>
                </div>
                <div class="ps">
                  <span>单位名称</span>
                  <Select class="ivSelect" v-model="UnitDialogData" style="width:200px">
                    <Option
                      v-for="item in UnitList"
                      :value="item.value"
                      :key="item.value"
                    >{{ item.label }}</Option>
                  </Select>
                </div>
                <div class="ps">
                  <span>角色</span>
                  <RadioGroup
                    calss="iv-input"
                    v-model="addsystem"
                    v-for="(item,index) in RoleList"
                    :key="index"
                  >
                    <Radio :key="index" :label="item.value" :value="item.value" :disabled="!isEdit">
                      <span>{{item.label}}</span>
                    </Radio>
                  </RadioGroup>
                </div>
              </Form>
            </div>
            <div slot="footer" style="text-align: center">
              <Button type="primary" @click="SaveUser">保存</Button>
            </div>
          </Modal>
        </div>
      </div>
    </div>
    <!--表格-->
    <Modal v-model="isDeleteDialog" width="360">
      <p slot="header" style="color:#f60;text-align:center">
        <Icon type="information-circled"></Icon>
        <span>删除确认</span>
      </p>
      <div style="text-align:center">
        <p>是否继续删除？</p>
      </div>
      <div slot="footer">
        <i-button type="error" size="large" long :loading="modal_loading" @click="delUser">删除</i-button>
      </div>
    </Modal>

    <Modal v-model="isResetDialog" width="360">
      <p slot="header" style="color:#f60;text-align:center">
        <Icon type="information-circled"></Icon>
        <span>重置密码确认</span>
      </p>
      <div style="text-align:center">
        <p>是否确认重置密码？</p>
      </div>
      <div slot="footer">
        <i-button type="info" size="large" long :loading="modal_loading_set" @click="doResetPWD">重置</i-button>
      </div>
    </Modal>

    <Modal v-model="isForbidDialog" width="360">
      <p slot="header" style="color:#f60;text-align:center">
        <Icon type="information-circled"></Icon>
        <span>状态修改确认</span>
      </p>
      <div style="text-align:center">
        <p>是否更改该账户状态？</p>
      </div>
      <div slot="footer">
        <i-button
          type="info"
          size="large"
          long
          :loading="modal_loading_forbid"
          @click="doForbidUser"
        >确认</i-button>
      </div>
    </Modal>

    <div>
      <Table border :columns="columns7" :data="UserTable" @on-sort-change="changeSort"></Table>
    </div>
    <!--分页器-->
    <div class="unltlistfyq">
      <Page
        :total="total"
        :current="current"
        :page-size="pageSize"
        @on-change="handleChangePage"
        show-elevator
      />
    </div>
  </div>
</template>

<script>
import {
  addUserList,
  queryUsersList,
  queryUnitName,
  queryRoleList,
  editUserInfo,
  forbiddenUser,
  deleteUserInfo,
  resetPWD
} from "@/api/usersManage";
import { queryUnitType, queryUnitData,quertMainOrganType } from "@/api/unitList";
import Cookies from "js-cookie";
import base64 from "js-base64";
export default {
  name: "usersManage",
  props: ["users"],

  computed: {
    fathusers(val) {
      return this.users;
    }
  },
  watch: {
    fathusers(val) {
      // console.log(val)
      //  this.UserTable=val
    }
  },

  data() {
    return {
      // 镇办
      TownToDo: [],
      OrganTypeList: [],
      // 区级部门
      Department: [],
      // 是否是编辑
      isEdit: true,
      // 分页总数
      total: null,
      // 当前页
      current: 1,
      // 显示条数
      pageSize: 15,
      sort: "",
      order: "asc",
      // 单位分类
      UnitClassification: null,
      UnitClassificationOption: [],
      // 添加单位分类
      addUnitClassificationArea: 1,
      addUnitClassificationOptionArea: [],
      addUnitClassificationPart: 3,
      addUnitClassificationOptionPart: [],
      // 职位分类
      PositionClassification: null,
      PositionClassificationOption: [],
      // 性别分类
      sex: null,
      sexOption: [
        {
          value: 1,
          label: "男"
        },
        {
          value: 2,
          label: "女"
        }
      ],
      modal10: false,
      // 角色
      addsystem: "系统管理员",
      // 账号id
      id: "",
      // 单位名称
      time: "",
      // 密码
      pwd: null,
      // 确认密码
      pwdagain: null,
      columns7: [
        {
          title: "账户ID",
          key: "id",
          sortable: true
          // render: (h, params) => {
          //   return h('div', [
          //     h('Icon', {
          //       props: {
          //         type: 'person'
          //       }
          //     }),
          //     h('strong', params.row.name)
          //   ]);
          // }
        },
        {
          title: "单位名称",
          key: "unit",
          sortable: true
        },
        {
          title: "身份",
          key: "borders",
          sortable: true
        },
        {
          title: "账户状态",
          key: "states ",
          render: (h, params) => {
            return h("span", this.formatUseState(params.row.states));
          }
        },
        {
          title: "创建时间",
          key: "time",
          sortable: true
        },
        {
          title: "操作",
          key: "action",
          width: 150,
          align: "center",
          render: (h, params) => {
            return h("div", [
              h(
                "Tooltip",
                {
                  props: {
                    placement: "top",
                    transfer: true,
                    content: "编辑"
                  }
                },
                [
                  h("Icon", {
                    props: {
                      type: "md-create" // 编辑
                    },
                    style: {
                      marginRight: "5px",
                      color: "#1296db",
                      fontSize: "18px",
                      cursor: "pointer"
                    },
                    on: {
                      click: () => {
                        this.show(params);
                      }
                    }
                  })
                ]
              ),

              h(
                "Tooltip",
                {
                  props: {
                    placement: "top",
                    transfer: true,
                    content: "重置密码"
                  }
                },
                [
                  h("Icon", {
                    props: {
                      type: "md-key" // 重置密码
                    },
                    style: {
                      marginRight: "5px",
                      color: "#1296db",
                      fontSize: "18px",
                      cursor: "pointer"
                    },
                    on: {
                      click: () => {
                        this.resetPWD(params);
                      }
                    }
                  })
                ]
              ),

              h(
                "Tooltip",
                {
                  props: {
                    placement: "top",
                    transfer: true,
                    content: params.row.states == true ? "禁用" : "启用"
                  }
                },
                [
                  h("Icon", {
                    props: {
                      type:
                        params.row.states == true ? "md-close" : "md-checkmark" // 禁用,启用
                    },
                    style: {
                      marginRight: "5px",
                      color: "#1296db",
                      fontSize: "18px",
                      cursor: "pointer"
                    },
                    on: {
                      click: () => {
                        this.forbidUser(params);
                      }
                    }
                  })
                ]
              ),

              h(
                "Tooltip",
                {
                  props: {
                    placement: "top",
                    transfer: true,
                    content: "删除"
                  }
                },
                [
                  h("Icon", {
                    props: {
                      type: "md-trash" // 删除
                    },
                    style: {
                      marginRight: "5px",
                      color: "red",
                      fontSize: "18px",
                      cursor: "pointer"
                    },
                    on: {
                      click: () => {
                        this.removeUser(params);
                      }
                    }
                  })
                ]
              )
            ]);
          }
        }
      ],
      UserTable: [],
      // 角色数组
      RoleList: [],
      // 单位名称
      UnitDialogData: null,
      UnitList: [],
      // 为了编辑
      UserEdit: null,
      GenderEdit: null,
      // 为了删除
      isDeleteDialog: false,
      isDeleteID: null,
      modal_loading: false,
      // 重置密码
      isResetDialog: false,
      isResetID: null,
      modal_loading_set: false,
      // 禁用启用
      isForbidDialog: false,
      isForbidID: null,
      isStates: null,
      modal_loading_forbid: false
    };
  },
  created() {
    debugger;
    quertMainOrganType().then(res => {
      debugger;
      if (res.IsSuccessful) {
        var row = res.Result.MainOrganTypeItemList;
        debugger;
        this.OrganTypeList = row;
      }
    });

    // 获取单位名称
    queryUnitName().then(res => {
      debugger;
      if (res.IsSuccessful == true) {
        this.UnitClassificationOption = [];
        var Option = [];
        Option.push({
          label: "全部",
          value: null
        });
        for (let i = 0; i < res.Result.OrganList.length; i++) {
          var tempObj = {};
          tempObj.label = res.Result.OrganList[i].OrganFullName;
          tempObj.value = res.Result.OrganList[i].OrganID;
          Option.push(tempObj);
        }
        var allOption = JSON.parse(JSON.stringify(Option));
        this.UnitClassificationOption = allOption;
        Option.shift();
        this.UnitList = Option;
      } else {
        this.$Message.error(res.Reason);
      }
    });
    // 查询用户列表
    this.queryUserList();
    // 获取角色数组
    this.getRoleList();
    // 获取单位分类
    this.getUnitType();
  },
  methods: {
    // 排序
    changeSort(key, type) {
      debugger;
      var sort = key.key;
      if (key.key == "borders") {
        sort = "RoleID";
      } else if (key.key == "id") {
        sort = "UserID";
      } else if (key.key == "states") {
        // sort = ''
      } else if (key.key == "time") {
        sort = "AddDate";
      } else if (key.key == "unit") {
        sort = "OrganizationName";
      }
      this.sort = sort;
      this.order = key.order;
      this.queryUserList();
    },
    // 翻页
    handleChangePage(val) {
      debugger;
      this.current = val;
      this.queryUserList();
    },
    // 获取单位分类
    getUnitType() {
      debugger;
      queryUnitType().then(res => {
        debugger;
        if (res.IsSuccessful == true) {
          var row = res.Result.CategoryList;
          // if(Cookies.get('RoleID') == 4){// 二级管理员
          //   for(let i = 0;i<row.length;i++){
          //     for(let j = 0; j < row[i].OrganTypeList.length;j++){
          //       if(Cookies.get('OrganTypeID') == row[i].OrganTypeList[j].OrganTypeID){
          //         row[i].OrganTypeList[j].disabled = false;
          //       }else{
          //         row[i].OrganTypeList[j].disabled = true;
          //       }
          //     }
          //   }
          // }else{
          //   for(let i = 0;i<row.length;i++){
          //     for(let j = 0;j< row[i].OrganTypeList.length;j++){
          //       row[i].disabled = false;
          //     }
          //   }
          // }
          this.TownToDo = row;
          // for(let i = 0; i < row.length; i++) {
          //   if(row[i].CategoryName == '镇办'){
          //     this.TownToDo = row[i].OrganTypeList;
          //   }else if(row[i].CategoryName == '区级部门'){
          //     this.Department = row[i].OrganTypeList;
          //   }
          // }
        } else {
          this.$Message.error(res.Reason);
        }
      });
    },
    // 格式化用户状态
    formatUseState(data) {
      if (data == true) {
        return "启用";
      } else if (data == false) {
        return "禁用";
      }
    },
    // 查询角色数组
    getRoleList() {
      debugger;
      this.RoleList = [];
      queryRoleList().then(res => {
        debugger;
        var Option = [];
        if (res.IsSuccessful == true) {
          for (let i = 0; i < res.Result.RoleList.length; i++) {
            var tempObj = {};
            tempObj.value = res.Result.RoleList[i].RoleID;
            tempObj.label = res.Result.RoleList[i].RoleName;
            Option.push(tempObj);
          }
          this.RoleList = Option;
        } else {
        }
      });
    },
    // 查询用户列表
    queryUserList() {
      debugger;
      var unitName = this.UnitClassification;
      var page = this.current;
      var pageSize = this.pageSize;
      var _data = {};
      if (unitName == null) {
        _data = {
          CurrentUserID: Cookies.get("UserID"),
          Page: page,
          PageSize: pageSize,
          Sort: this.sort,
          Order: this.order
        };
      } else {
        _data = {
          CurrentUserID: Cookies.get("UserID"),
          Page: page,
          PageSize: pageSize,
          OrganizationID: unitName,
          Sort: this.sort,
          Order: this.order
        };
      }
      // 获取数据
      queryUsersList(_data).then(res => {
        debugger;
        if (res.IsSuccessful == true) {
          this.total = res.Result.Total;
          var tableList = [];
          for (let i = 0; i < res.Result.UserInfoList.length; i++) {
            var tempObj = {};
            tempObj.id = res.Result.UserInfoList[i].UserToken; // 账户ID
            tempObj.unit = res.Result.UserInfoList[i].OrganizationName; // 单位名称
            tempObj.borders = res.Result.UserInfoList[i].RoleName; // 身份
            tempObj.states = res.Result.UserInfoList[i].UseStatus; // 账户状态
            tempObj.time = res.Result.UserInfoList[i].AddDate; // 创建时间
            tempObj.Gender = res.Result.UserInfoList[i].Gender; // 性别：1男2女
            tempObj.LastUpdateUserID =
              res.Result.UserInfoList[i].LastUpdateUserID; // 最后一次更新人
            tempObj.OrganizationID = res.Result.UserInfoList[i].OrganizationID; // 单位ID
            tempObj.RoleID = res.Result.UserInfoList[i].RoleID; // 角色ID
            tempObj.UserID = res.Result.UserInfoList[i].UserID; // 用户ID
            tempObj.OrganTypeID = res.Result.UserInfoList[i].OrganTypeID; // 单位类型ID
            tableList.push(tempObj);
          }
          this.UserTable = tableList;
        } else {
          this.$Message.error(res.Reason);
        }
      });
    },
    // 点击单位分类
    handClickUnitPart(id) {
      debugger;
      var _data = {
        OrganTypeID: id,
        Keyword: null,
        Sort: "",
        Order: "asc",
        Page: 1,
        PageSize: 500
      };
      // 请求接口
      queryUnitData(_data).then(res => {
        debugger;
        if (res.IsSuccessful == true) {
          this.UnitList = [];
          var option = [];
          for (let i = 0; i < res.Result.OrganInfoList.length; i++) {
            var tempObj = {};
            tempObj.label = res.Result.OrganInfoList[i].OrganFullName;
            tempObj.value = res.Result.OrganInfoList[i].OrganID;
            option.push(tempObj);
          }
          this.UnitList = option;
        } else {
          this.$Message.error(res.Reason);
        }
      });
    },
    // 新建用户
    SaveUser() {
      debugger;
      if (this.isEdit == true) {
        var UserToken = this.id;
        var PWD = this.pwd;
        var PWDAgin = this.pwdagain;
        // var Gender = parseInt(this.area);
        var OrganizationID = parseInt(this.UnitDialogData);
        var RoleID = parseInt(this.addsystem);
        var AddUserID = Cookies.get("UserID");
        // 判断是不是两次密码一致
        if (PWD != PWDAgin) {
          this.$Message.warning("密码和确认密码不一致，请重新输入");
          return false;
        }
        PWD = Base64.encode(this.pwd);
        var data = {
          UserToken,
          PWD,
          // Gender,
          OrganizationID,
          RoleID,
          AddUserID
        };
        addUserList(data).then(res => {
          debugger;
          if (res.IsSuccessful == true) {
            this.$Message.success("添加成功！");
            // 查询用户列表
            this.queryUserList();
            this.modal10 = false;
          } else {
            // this.$Message.error('添加失败！');
            this.$Message.error(res.Reason);
          }
        });
      } else {
        var _data = {
          UserID: this.UserEdit,
          UserToken: this.id,
          // Gender: this.GenderEdit,
          // Gender: parseInt(this.area),
          OrganizationID: this.UnitDialogData,
          RoleID: this.addsystem,
          LastUpdateUserID: Cookies.get("UserID")
        };
        // 编辑接口
        editUserInfo(_data).then(res => {
          debugger;
          if (res.IsSuccessful == true) {
            this.$Message.success("编辑成功！");
            // 查询用户列表
            this.queryUserList();
            this.modal10 = false;
          } else {
            // this.$Message.error('编辑失败！');
            this.$Message.error(res.Reason);
          }
        });
      }
    },
    // 新建用户
    xjbtn() {
      debugger;
      this.$refs.formInline.resetFields();
      this.isEdit = true;
      this.modal10 = true;
      // 置空数据
      this.id = null;
      this.time = null;
      this.pwd = null;
      this.pwdagain = null;
      this.area = null;
      this.UnitDialogData = null;
      this.addsystem = null;
    },
    // 编辑用户
    show(val) {
      debugger;
      this.isEdit = false;
      console.log(val.row.id);
      this.id = val.row.id;
      this.UnitDialogData = val.row.OrganizationID;
      // this.time=val.row.time;
      this.addsystem = val.row.RoleID;
      // 不可编辑
      this.UserEdit = val.row.UserID;
      // this.GenderEdit = val.row.Gender;
      this.area = val.row.OrganTypeID;
      this.modal10 = true;
    },
    // 重置密码
    resetPWD(val) {
      debugger;
      var UserID = val.row.UserID;
      this.isResetDialog = true;
      this.isResetID = UserID;
      this.modal_loading_set = false;
    },
    // 执行重置密码
    doResetPWD() {
      debugger;
      this.modal_loading_set = true;
      var UserID = this.isResetID;
      var _data = {
        UserID
      };
      resetPWD(_data).then(res => {
        debugger;
        this.modal_loading_set = false;
        if (res.IsSuccessful == true) {
          this.$Message.success("重置为初始密码成功！初始化密码为：000000");
          this.isResetDialog = false;
          // 查询用户列表
          this.queryUserList();
        } else {
          // this.$Message.error('重置为初始密码失败！');
          this.$Message.error(res.Reason);
        }
      });
    },
    // 删除用户
    removeUser(val) {
      debugger;
      var UserID = val.row.UserID;
      this.isDeleteID = UserID;
      this.isDeleteDialog = true;
      this.modal_loading = false;
    },
    // 执行删除
    delUser() {
      debugger;
      this.modal_loading = true;
      var _data = {
        UserID: this.isDeleteID
      };
      deleteUserInfo(_data).then(res => {
        debugger;
        this.modal_loading = false;
        if (res.IsSuccessful == true) {
          this.$Message.success("删除成功！");
          this.isDeleteDialog = false;
          // 查询用户列表
          this.queryUserList();
        } else {
          // this.$Message.error('删除失败！');
          this.$Message.error(res.Reason);
        }
      });
    },
    // 禁用用户
    forbidUser(val) {
      debugger;
      var id = val.row.UserID;
      var isShow = val.row.states;
      if (isShow == true) {
        // 变成禁用
        isShow = false;
      } else if (isShow == false) {
        // 变成启用
        isShow = true;
      }
      this.isForbidDialog = true;
      this.isForbidID = id;
      this.isStates = isShow;
      this.modal_loading_forbid = false;
    },
    // 执行禁用用户操作
    doForbidUser() {
      debugger;
      var _data = {
        UserID: this.isForbidID,
        UseStatus: this.isStates
      };
      this.modal_loading_forbid = true;
      forbiddenUser(_data).then(res => {
        debugger;
        this.modal_loading_forbid = false;
        if (res.IsSuccessful == true) {
          if (this.isStates == true) {
            this.$Message.success("启用成功！");
            this.isForbidDialog = false;
            // 查询用户列表
            this.queryUserList();
          } else {
            this.$Message.success("禁用成功！");
            this.isForbidDialog = false;
            // 查询用户列表
            this.queryUserList();
          }
        } else {
          if (this.isStates == true) {
            // this.$Message.success('启用成功！');
            this.$Message.error(res.Reason);
            // this.isForbidDialog = false;
            // // 查询用户列表
            // this.queryUserList();
          } else {
            // this.$Message.success('禁用成功！');
            this.$Message.error(res.Reason);
            // this.isForbidDialog = false;
            // // 查询用户列表
            // this.queryUserList();
          }
        }
      });
    },
    remove(index) {
      this.UserTable.splice(index, 1);
    }
  }
};
</script>

<style scoped>
.craeteTop {
  width: 100%;
  height: 65px;
  background: #cdd0d4;
  line-height: 65px;
  padding: 0 60px;
  color: #323232;
}
.craeteTop p {
  float: left;
  font-size: 24px;
  font-weight: 600;
}
.logHeader {
  margin: 0 15px;
  height: 70px;
  line-height: 70px;
  /* display: flex; */
}
.logHeader span {
  font-size: 14px;
  color: #515a6e;
}

.unltlistfyq {
  text-align: right;
  margin-right: 15px;
  height: 100px;
  line-height: 100px;
}
.unltlistInput {
  margin-left: 10px;
}
.ivSelect {
  margin: 0 45px 0 10px;
}
.primary {
  margin-left: 30px;
}
.ivButton {
  float: right;
}
.ivButtons {
  width: 100px;
  height: 40px;
  background: #2d8cf0;
  border-radius: 5px;
  color: #fff;
  border: none;
}
.ps {
  width: 100%;
  height: 60px;
  line-height: 60px;
  text-align: center;
  display: -webkit-flex;
  -webkit-align-items: center;
}
.ps > span {
  width: 20%;
  font-size: 16px;
  color: black;
  margin-right: 30px;
  display: block;
}
.ps .iv-input {
  width: 80%;
  display: block;
}
.unltlistfyq {
  text-align: right;
  margin-right: 15px;
  height: 100px;
  line-height: 100px;
}
</style>
<style>
.ivu-table-wrapper {
  margin-top: 2px;
}
</style>
