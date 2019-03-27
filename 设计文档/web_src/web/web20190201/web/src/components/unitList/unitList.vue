<template>
  <div>
    <div class="craeteTop">单位列表</div>
    <div class="unltlistBot">
      <!--所属分类-->
      <div class="unitLIstFl">
        <div>
          <span class="unitLIstspan">所属分类</span>
          <Select v-model="model1" style="width:200px">
            <Option v-for="item in cityList" :value="item.value" :key="item.value">{{ item.label }}</Option>
          </Select>
          <!--属搜input go-->
          <span class="unitLIstspantwo">关键字</span>
          <Input
            class="unltlistInput"
            style="width:260px;"
            v-model="value1"
            placeholder="搜索单位编码、单位全称、单位简称..."
          />
          <Button type="primary" @click="getUnitData">搜索</Button>
        </div>
        <div>
          <!--<Button type="primary" to="/CreateLeader">创建单位</Button>-->
          <Button @click="clickAdd" style="background: #2d8cf0 ;color:#fff">创建单位</Button>
          <Modal v-model="modal2" width="550">
            <p slot="header" style="color:#2d8cf0;text-align:center">创建单位</p>
            <div style="text-align:center">
              <!-- <Form :rules="rules"> -->
              <div class="inputas">
                <span>单位编码</span>
                <div class="inputasrig">
                  <Input class="inputs" v-model="unitNo" placeholder="请输入..."/>
                  <p>单位编码为单位唯一识别</p>
                </div>
              </div>
              <!-- <FormItem prop="unitFull"> -->
              <div class="inputas">
                <span>单位全称</span>
                <div class="inputasrig">
                  <Input class="inputs" v-model="unitName" placeholder="请输入..."/>
                  <p>单位全称，最长不能超过20个字</p>
                </div>
              </div>
              <!-- </FormItem> -->
              <div class="inputas" v-if="isUnitShortNameShow">
                <span>单位简称</span>
                <div class="inputasrig">
                  <Input class="inputs" v-model="unitSimpleName" placeholder="请输入..."/>
                  <p>单位的简称，最多不能超过10个字</p>
                </div>
              </div>
              <div class="inputas" v-if="isUnitAreaShow">
                <span>地区选择</span>
                <div class="inputasrig">
                  <Select v-model="areaID">
                    <Option
                      v-for="item in areaList"
                      :value="item.AreaID"
                      :key="item.AreaID"
                    >{{ item.AreaName }}</Option>
                  </Select>
                </div>
              </div>
              <!--镇办 区政府-->
              <div class="createZb">
                <Tabs :animated="false">
                  <template v-for="(item,index) in TownToDo">
                    <!-- <TabPane label="镇办" name="镇办" >
                      <div>
                        <RadioGroup  v-model="part">
                          <template v-for="(item, index) in TownToDo">
                            <Radio :key="index" :label="item.OrganTypeName"></Radio>
                          </template>
                        </RadioGroup>
                      </div>
                    </TabPane>
                    <TabPane label="区级部门" name="区级部门" >
                      <div>
                        <RadioGroup v-model="part">
                          <template v-for="(item, index) in Department">
                            <Radio :key="index" :label="item.OrganTypeName"></Radio>
                          </template>
                        </RadioGroup>
                      </div>
                    </TabPane>-->
                    <TabPane :key="index" :label="item.CategoryName" :name="item.CategoryName">
                      <div>
                        <RadioGroup v-model="part">
                          <template v-for="(cell, cellIndex) in item.OrganTypeList">
                            <!-- <Radio :key="cellIndex" :label="cell.OrganTypeName"></Radio> -->
                            <Radio
                              :key="cellIndex"
                              :label="cell.OrganTypeID"
                              :value="cell.OrganTypeID"
                            >
                              <span>{{cell.OrganTypeName}}</span>
                            </Radio>
                          </template>
                        </RadioGroup>
                      </div>
                    </TabPane>
                  </template>
                </Tabs>
              </div>
              <!-- </Form> -->
            </div>
            <div slot="footer" style="text-align: center">
              <!--<Button type="error" size="large" long :loading="modal_loading" @click="del">Delete</Button>-->
              <Button type="primary" @click="addUnit">创建</Button>
            </div>
          </Modal>
        </div>
      </div>
      <!--单位明细列表-->
      <div>
        <Table border :columns="columns7" :data="data6" @on-sort-change="changeSort"></Table>
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

    <Modal v-model="isDeleteDialog" width="360">
      <p slot="header" style="color:#f60;text-align:center">
        <Icon type="information-circled"></Icon>
        <span>删除确认</span>
      </p>
      <div style="text-align:center">
        <p>是否继续删除？</p>
      </div>
      <div slot="footer">
        <i-button type="error" size="large" long :loading="modal_loading" @click="doDelUnit">删除</i-button>
      </div>
    </Modal>
  </div>
</template>

<script>
import {
  addUnit,
  queryUnitType,
  queryUnitData,
  GetAreaList,
  deleteUnitInfo
} from "@/api/unitList";
import Cookies from "js-cookie";
export default {
  name: "unitList",
  props: ["tn"],
  computed: {
    fathathData(val) {
      debugger;
      console.log(this.tn);
      return this.tn;
    }
  },
  watch: {
    fathathData(val) {
      debugger;
      console.log(this.tn);
      // this.data6=val
    }
  },
  data() {
    return {
      // 地区选择id
      areaID: null,
      // 总数
      total: 0,
      // 当前页
      current: 1,
      // 显示条数
      pageSize: 15,
      sort: "",
      order: "asc",
      value1: "",
      modal2: false,
      cityList: [],
      data6: [],
      model1: null,
      columns7: [
        {
          title: "单位编码",
          key: "a",
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
          title: "单位全称",
          key: "b",
          sortable: true
        },
        // {
        //   title: "单位简称",
        //   key: "c",
        //   sortable: true
        // },
        {
          title: "所属分类",
          key: "d"
        },
        {
          title: "干部组成",
          key: "e",
          sortable: true
        },
        {
          title: "创建时间",
          key: "f",
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
                      type: "md-create"
                    },
                    style: {
                      marginRight: "5px",
                      color: "#2d8cf0",
                      fontSize: "18px",
                      cursor: "pointer"
                    },
                    on: {
                      click: evt => {
                        debugger;
                        // 获取值
                        console.log(params);
                        var data = {
                          a: params.row.a,
                          b: params.row.b,
                          c: params.row.c,
                          d: params.row.d,
                          e: params.row.e,
                          f: params.row.f,
                          id: params.row.id,
                          OrganTypeID: params.row.OrganTypeID,
                          areaID: params.row.areaID
                          // areaName: params.row.areaName
                        };
                        this.$router.push({
                          path: "chinaCompany",
                          query: data
                        });
                      }
                    }
                  })
                ]
              ),
              // add by lwj---2018.12.19---添加删除操作
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
                      type: "md-trash"
                    },
                    style: {
                      marginRight: "5px",
                      color: "red",
                      fontSize: "18px",
                      cursor: "pointer"
                    },
                    on: {
                      click: () => {
                        debugger;
                        this.deleteUnit(params);
                      }
                    }
                  })
                ]
              )
            ]);
          }
        }
      ],
      // add by lwj---2018.12.19
      unitNo: null,
      unitName: null,
      unitSimpleName: null,
      // part:'重点发展区域镇办',
      partBackUp: null,
      // 镇办
      TownToDo: [],
      // 区级部门
      Department: [],
      //  地区list
      areaList: [],
      // 删除
      isDeleteDialog: false,
      isDeleteID: null,
      modal_loading: false,

      isUnitShortNameShow: false,
      isUnitAreaShow: false
      // rules:{
      //   unitFull: [
      //     { type: 'string', max: 20, message: '长度不能超过20位', trigger: 'blur' }
      //   ],
      // }
    };
  },
  created() {
    // 查询单位所属分类
    queryUnitType().then(res => {
      debugger;
      this.cityList = [];
      if (res.IsSuccessful == true) {
        var row = res.Result.CategoryList;
        var selectData = [];
        selectData.push({
          label: "全部",
          value: null
        });
        for (let i = 0; i < row.length; i++) {
          for (let j = 0; j < row[i].OrganTypeList.length; j++) {
            var tempObj = {};
            tempObj.label = row[i].OrganTypeList[j].OrganTypeName;
            tempObj.value = row[i].OrganTypeList[j].OrganTypeID;
            selectData.push(tempObj);
            //备份首选项
            if (i == 0 && j == 0) {
              this.partBackUp = tempObj.label =
                row[i].OrganTypeList[j].OrganTypeName;
              this.part = this.partBackUp;
            }
          }
        }
        this.cityList = selectData;
        // for(let i = 0; i < row.length; i++) {
        //   if(row[i].CategoryName == '镇办'){
        //     this.TownToDo = row[i].OrganTypeList;
        //   }else if(row[i].CategoryName == '区级部门'){
        //     this.Department = row[i].OrganTypeList;
        //   }
        // }
        debugger;
        this.TownToDo = row;
        var a = this.TownToDo;
        this.part = this.TownToDo.OrganTypeList[0].OrganTypeName;
      } else {
        this.$Message.error(res.Reason);
      }
    });
    // 查询单位列表数据
    this.getUnitData();
    // 获取地区列表
    this.getAreaList();
  },
  methods: {
    // 排序
    changeSort(key, type) {
      debugger;
      var sort = key.key;
      if (key.key == "a") {
        sort = "OrganCode";
      } else if (key.key == "b") {
        sort = "OrganFullName";
      } else if (key.key == "c") {
        sort = "OrganShortName";
      } else if (key.key == "e") {
        sort = "OfficerQuanlity";
      } else if (key.key == "f") {
        sort = "AddDate";
      }
      this.sort = sort;
      this.order = key.order;
      this.getUnitData();
    },
    // 翻页
    handleChangePage(val) {
      debugger;
      this.current = val;
      this.getUnitData();
    },
    // 删除单位
    deleteUnit(val) {
      debugger;
      var unitID = val.row.id;
      this.isDeleteID = unitID;
      this.isDeleteDialog = true;
      this.modal_loading = false;
    },
    // 执行删除操作
    doDelUnit() {
      debugger;
      this.modal_loading = true;
      var _data = {
        OrganID: this.isDeleteID,
        CurrentUserID: Cookies.get("UserID")
      };
      deleteUnitInfo(_data).then(res => {
        debugger;
        this.modal_loading = false;
        if (res.IsSuccessful == true) {
          this.$Message.success("删除成功！");
          this.isDeleteDialog = false;
          // 查询
          this.getUnitData();
        } else {
          // this.$Message.error('删除失败！');
          this.$Message.error(res.Reason);
        }
      });
    },
    // 获取地区列表
    getAreaList() {
      debugger;
      GetAreaList().then(res => {
        debugger;
        if (res.IsSuccessful == true) {
          var row = res.Result.AreaItemList;
          this.areaList = row;
        } else {
          this.$Message.error(res.Reason);
        }
      });
    },
    // 查询
    getUnitData() {
      debugger;
      // var a = this.model1;
      var _data = {
        OrganTypeID: this.model1,
        Keyword: this.value1,
        Sort: this.sort,
        Order: this.order,
        Page: this.current,
        PageSize: this.pageSize,
        Sort: this.sort,
        Order: this.order
      };
      // 查询单位列表数据
      queryUnitData(_data).then(res => {
        debugger;
        this.data6 = [];
        if (res.IsSuccessful == true) {
          this.total = res.Result.Total;
          var listData = [];
          for (let i = 0; i < res.Result.OrganInfoList.length; i++) {
            var tempObj = {};
            tempObj.a = res.Result.OrganInfoList[i].OrganCode; // 单位编码
            tempObj.b = res.Result.OrganInfoList[i].OrganFullName; // 单位全称
            tempObj.c = res.Result.OrganInfoList[i].OrganShortName; // 单位简称
            tempObj.d = res.Result.OrganInfoList[i].OrganTypeName; // 单位分类名称
            tempObj.e = res.Result.OrganInfoList[i].OfficerQuanlity + "人"; // 干部组成
            tempObj.f = res.Result.OrganInfoList[i].AddDate; // 创建时间
            tempObj.id = res.Result.OrganInfoList[i].OrganID; // id
            tempObj.OrganTypeID = res.Result.OrganInfoList[i].OrganTypeID; // 部门小类
            tempObj.areaID = res.Result.OrganInfoList[i].AreaID; // 地区id
            // tempObj.areaName = res.Result.OrganInfoList[i].AreaName;// 地区名称
            listData.push(tempObj);
          }
          this.data6 = listData;
        } else {
          this.$Message.error(res.Reason);
        }
      });
    },
    selectBtn(val) {
      console.log(val);
      this.$emit("btn6", val);
    },
    // 枚举
    enumOrgType(data) {
      let val = "";
      if (data == "重点发展区域镇办") {
        val = 1;
      } else if (data == "生态发展区域镇办") {
        val = 3;
      } else if (data == "党群部门") {
        val = 4;
      } else if (data == "政府经济部门") {
        val = 5;
      } else if (data == "政府非经济部门") {
        val = 6;
      } else if (data == "驻区单位") {
        val = 7;
      }
      return val;
    },
    // 点击创建单位按钮
    clickAdd() {
      debugger;
      // 置空
      this.unitNo = null;
      this.unitName = null;
      this.unitSimpleName = null;
      this.areaID = null;
      this.part = this.partBackUp;
      this.modal2 = true;
    },
    // 创建单位
    addUnit() {
      debugger;
      var unitNo = this.unitNo;
      var unitName = this.unitName;
      var unitSimpleName = this.unitSimpleName;
      // var part = this.enumOrgType(this.part);
      var part = this.part;
      var areaID = this.areaID;
      var form = {
        OrganCode: unitNo,
        OrganFullName: unitName,
        OrganShortName: unitSimpleName,
        OrganTypeID: part,
        AreaID: areaID,
        AddUserID: Cookies.get("UserID")
      };
      addUnit(form).then(res => {
        debugger;
        if (res.IsSuccessful == true) {
          this.$Message.success("添加成功！");
          this.modal2 = false;
          this.getUnitData();
        } else {
          this.$Message.error(res.Reason);
        }
      });
    }
  }
};
</script>

<style scoped>
.craeteTop {
  width: 100%;
  height: 65px;
  font-size: 24px;
  font-weight: 600;
  background: #cdd0d4;
  line-height: 65px;
  padding-left: 60px;
  color: #323232;
}

.unitLIstFl {
  padding: 0 15px;
  height: 70px;
  line-height: 70px;
  display: -webkit-flex;
  justify-content: space-between;
}
.ivu-select-selection {
  height: 40px;
}

.unitLIstspan {
  margin-right: 10px;
  font-size: 14px;
  color: #515a6e;
}
.unltlistInput {
  width: 180px;
  margin: 0px 30px 0 0px;
}
.unitLIstspantwo {
  margin: 0 10px 0 70px;
  font-size: 14px;
  color: #515a6e;
}
.unltlistfyq {
  text-align: right;
  margin-right: 15px;
  height: 100px;
  line-height: 100px;
}
.createBtLf .inputas .inputs {
  width: 50vh;
  line-height: 30px;
}
.inputas {
  margin-top: 30px;
  display: -webkit-flex;
  -webkit-justify-content: center;
}
.inputas span {
  margin-right: 20px;
  margin-top: 5px;
  display: block;
}
.inputas p {
  font-size: 14px;
  margin-top: 10px;
}
.inputasrig {
  text-align: left;
  width: 300px;
}
.createZb {
  margin-top: 30px;
}
.createAn {
  margin-top: 40px;
}
</style>
<style>
.ivu-table-wrapper {
  margin: 0 15px;
}
.createZb .ivu-tabs-bar {
  border-bottom: 0px solid #dcdee2;
}
.ivu-tabs-nav {
  float: none;
  font-size: 16px;
}

.ivu-radio-wrapper {
  font-size: 14px;
}
.ivu-tabs-nav-scroll {
  display: inline-block;
}
.ivu-input-large {
  height: 30px;
}
</style>
