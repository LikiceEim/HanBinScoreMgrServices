<template>
  <div class="china-company">
    <div class="craeteTop">{{title}}</div>
    <div class="comCenter">
      <div class="cenLeft">
        <div class="createBt">
          <!--创建input-->
          <div class="inputas">
            <span>单位编码</span>
            <div class="inputasrig">
              <Input
                class="inputs"
                style="width:420px"
                v-model="value1"
                @on-blur="btn1"
                placeholder="请输入..."
              />
              <p>单位的编码为单位唯一识别ID, 不能重复</p>
            </div>
          </div>
          <div class="inputas">
            <span>单位全称</span>
            <div class="inputasrig">
              <Input
                class="inputs"
                style="width:420px"
                v-model="value2"
                @on-blur="btn2"
                placeholder="请输入..."
              />
              <p>单位的全称，最长不能超过20个字</p>
            </div>
          </div>
          <div class="inputas" v-if="isUnitShortNameShow">
            <span>单位简称</span>
            <div class="inputasrig">
              <Input
                class="inputs"
                style="width:420px"
                v-model="value3"
                @on-blur="btn3"
                placeholder="请输入..."
              />
              <p>单位的简称，最多不能超过10个字</p>
            </div>
          </div>
          <div class="inputas" v-if="isUnitAreaShow">
            <span>地区选择</span>
            <div class="inputasrig">
              <Select v-model="areaID" style="width:420px;">
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
            <div>
              <RadioGroup v-model="orgCategory">
                <template v-for="(cell, cellIndex) in OrganTypeList">
                  <Radio :key="cellIndex" :label="cell.OrganTypeID" :value="cell.OrganTypeID">
                    <span>{{cell.OrganTypeName}}</span>
                  </Radio>
                </template>
              </RadioGroup>
            </div>

            <Tabs :animated="false" @on-click="tabBtn1" v-if="false">
              <template v-for="(item,index) in TownToDo">
                <!-- <TabPane label="镇办" name="镇办" >
                    <div>
                      <RadioGroup v-model="orgCategory">
                        <template v-for="(item, index) in TownToDo">
                          <Radio :key="index" :label="item.OrganTypeName"></Radio>
                        </template>
                      </RadioGroup>
                    </div>
                  </TabPane>
                  <TabPane label="区级部门" name="区级部门">
                    <div>
                      <RadioGroup v-model="orgCategory" @on-change="butt2"  >
                        <template v-for="(item, index) in Department">
                          <Radio :key="index" :label="item.OrganTypeName"></Radio>
                        </template>
                      </RadioGroup>
                    </div>
                </TabPane>-->
                <TabPane :key="index" :label="item.CategoryName" :name="item.CategoryName">
                  <div>
                    <RadioGroup v-model="orgCategory">
                      <template v-for="(cell, cellIndex) in item.OrganTypeList">
                        <Radio :key="cellIndex" :label="cell.OrganTypeID" :value="cell.OrganTypeID">
                          <span>{{cell.OrganTypeName}}</span>
                        </Radio>
                      </template>
                    </RadioGroup>
                  </div>
                </TabPane>
              </template>
            </Tabs>
          </div>
          <!--创建按钮-->
          <div class="createAn">
            <Button type="primary" size="large" @click="doUpdataRow">更新</Button>
            <div>
              <Button size="large" @click="cancelRow">取消</Button>
              <!--<Modal-->
              <!--v-model="modal1"-->
              <!--title="删除单位"-->
              <!--@on-ok="ok"-->
              <!--@on-cancel="cancel">-->
              <!--<p class="createAns">确定删除该单位信息？</p>-->
              <!--<p>删除后单位旗下相应干部信息就不存在了！谨慎操作</p>-->
              <!--<p>-->
              <!--<Button type="primary" size="large">更新</Button>-->
              <!--<Button size="large" >删除</Button>-->
              <!--</p>-->
              <!--</Modal>-->
              <Modal v-model="modal1" width="500" title="删除单位" @on-ok="ok" @on-cancel="cancel">
                <div style="text-align:center">
                  <p class="createAns">确定删除该单位信息？</p>
                  <p>删除后单位旗下相应干部信息就不存在了！谨慎操作</p>
                </div>
                <div slot="footer">
                  <Button type="primary" size="large" @click="doUpdataRow">更新</Button>
                  <Button size="large" @click="cancelRow">取消</Button>
                </div>
              </Modal>
            </div>
          </div>
        </div>
      </div>
      <div class="cenRig">
        <div class="cennum">
          <span>干部总人数</span>
          <h3>{{total}}</h3>
        </div>
        <!--单位明细列表-->
        <div>
          <Table border :columns="columns1" :data="data1" tooltip></Table>
        </div>
        <div class="cenMore">
          <Button type="primary" size="large" @click="go()">更多 >></Button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Cookies from "js-cookie";
import {
  GetAreaList,
  queryUnitType,
  updateUnitInfo,
  quertDetailUnitInfo,
  quertMainOrganType
} from "@/api/unitList";
import { quertLeaderList } from "@/api/leaderList";
export default {
  name: "chinaCompany",
  data() {
    return {
      // 总人数
      total: null,
      TownToDo: [],
      Department: [],
      // id
      id: null,
      title: null,
      value1: "",
      value2: "",
      value3: "",
      type: null,
      list: [],
      modal1: false,
      columns1: [
        {
          title: "姓名",
          key: "name"
        },
        {
          title: "性别",
          key: "sex",
          width: 50,
          render: (h, params) => {
            return h("span", this.formatSex(params.row.sex));
          }
        },
        {
          title: "出生年月",
          key: "birth",
          width: 110,
          render: (h, params) => {
            return h("span", this.formatTime(params.row.birth));
          }
        },
        {
          title: "现任职务",
          key: "present"
        },
        {
          title: "级别",
          key: "rank"
        },
        {
          title: "任职时间",
          key: "time",
          width: 110,
          render: (h, params) => {
            return h("span", this.formatTime(params.row.time));
          }
        },
        {
          title: "积分",
          key: "integral",
          width: 70
        }
      ],
      data1: [],
      // 部门小类
      orgCategory: null,
      areaID: null,
      areaList: [],
      areaName: null,
      isUnitShortNameShow: false,
      isUnitAreaShow: false,

      //单位类型
      OrganTypeList: []
    };
  },
  created() {
    debugger;
    var data = this.$route.query;
    this.value1 = data.a; // 单位编码
    this.value2 = data.b; // 单位全称
    this.value3 = data.c; // 单位简称
    this.id = data.id; // id
    this.title = data.b; // 标题
    var type = data.OrganTypeID; // 部门小类
    this.type = type;
    // var typeString = this.enumOrg(type);
    // this.orgCategory = typeString;
    this.orgCategory = type;
    this.areaID = data.areaID;
    // this.areaName = data.areaName;
    // 获取地区列表
    this.getAreaList();
    // 获取单位分类
    this.getUnitType();
    // 获取干部列表
    // this.getLeaderList();
    this.getDetailInfo();

    //获取单位类型
    quertMainOrganType().then(res => {
      debugger;
      if (res.IsSuccessful) {
        var row = res.Result.MainOrganTypeItemList;
        
        this.OrganTypeList = row;
      }
    });
  },
  methods: {
    // 格式化性别
    formatSex(data) {
      if (data == 1) {
        return "男";
      } else if (data == 2) {
        return "女";
      }
    },
    // 格式化时间
    formatTime(date) {
      debugger;
      var newDate = date.split(" ");
      newDate = newDate[0];
      return newDate;
    },
    // 获取单位详情
    getDetailInfo() {
      debugger;
      var _data = {
        OrganID: this.id,
        CurrentUserID: Cookies.get("UserID")
      };
      quertDetailUnitInfo(_data).then(res => {
        debugger;
        if (res.IsSuccessful == true) {
          var row = res.Result.OfficerList;
          this.total = res.Result.Total;
          var list = [];
          for (let i = 0; i < row.length; i++) {
            var tempObj = {};
            tempObj.name = row[i].Name; // 姓名
            tempObj.sex = row[i].Gender; // 性别：1男2女
            tempObj.birth = row[i].Birthday; // 出生年月
            // tempObj.unit = row[i].OrganizationName;// 所在单位
            tempObj.present = row[i].PositionName; // 现任职务/
            tempObj.rank = row[i].LevelName; // 级别
            tempObj.time = row[i].OnOfficeDate; // 任职时间
            tempObj.integral = row[i].CurrentScore; // 积分
            // tempObj.IdentifyNumber = row[i].IdentifyNumber;// 身份证号
            // tempObj.LevelID = row[i].LevelID;// 级别ID
            // tempObj.OfficerID = row[i].OfficerID;// 干部ID
            // tempObj.OrganizationID = row[i].OrganizationID;// 所在单位ID
            // tempObj.PositionID = row[i].PositionID;// 职务ID
            list.push(tempObj);
          }
          this.data1 = list;
        } else {
          this.$Message.error(res.Reason);
        }
      });
    },
    // 获取干部列表
    getLeaderList() {
      debugger;
      var _data = {
        Page: 1,
        PageSize: 15
      };
      quertLeaderList(_data).then(res => {
        debugger;
        if (res.IsSuccessful == true) {
          var row = res.Result.OfficerInfoList;
          this.total = res.Result.Total;
          var list = [];
          for (let i = 0; i < row.length; i++) {
            var tempObj = {};
            tempObj.name = row[i].Name; // 姓名
            tempObj.sex = row[i].Gender; // 性别：1男2女
            tempObj.birth = row[i].Birthday; // 出生年月
            tempObj.unit = row[i].OrganizationName; // 所在单位
            tempObj.present = row[i].PositionName; // 现任职务
            tempObj.rank = row[i].LevelName; // 级别
            tempObj.time = row[i].OnOfficeDate; // 任职时间
            tempObj.integral = row[i].CurrentScore; // 积分
            tempObj.IdentifyNumber = row[i].IdentifyNumber; // 身份证号
            tempObj.LevelID = row[i].LevelID; // 级别ID
            tempObj.OfficerID = row[i].OfficerID; // 干部ID
            tempObj.OrganizationID = row[i].OrganizationID; // 所在单位ID
            tempObj.PositionID = row[i].PositionID; // 职务ID
            list.push(tempObj);
          }
          this.data1 = list;
        } else {
          this.$Message.error(res.Reason);
        }
      });
    },
    // 获取单位分类
    getUnitType() {
      debugger;
      queryUnitType().then(res => {
        debugger;
        if (res.IsSuccessful == true) {
          var row = res.Result.CategoryList;
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
    // 更新操作
    doUpdataRow() {
      debugger;
      var OrganID = this.id; // id
      var OrganCode = this.value1; // 单位编码
      var OrganFullName = this.value2; // 单位全称
      var OrganShortName = this.value3; // 单位简称
      // var OrganTypeID = this.inverseEnumOrg(this.orgCategory);
      var OrganTypeID = this.orgCategory;
      var UpdateUserID = Cookies.get("UserID");
      var AreaID = this.areaID;
      var CurrentUserID = Cookies.get("UserID");
      var _data = {
        OrganID,
        OrganCode,
        OrganFullName,
        OrganShortName,
        OrganTypeID,
        UpdateUserID,
        AreaID,
        CurrentUserID
      };
      updateUnitInfo(_data).then(res => {
        debugger;
        if (res.IsSuccessful == true) {
          this.$Message.success("更新成功！");
        } else {
          // this.$Message.error('更新失败！');
          this.$Message.error(res.Reason);
        }
      });
    },
    // 取消
    cancelRow() {
      debugger;
      this.$router.push("UnitList");
    },
    // 反枚举
    inverseEnumOrg(data) {
      let val = null;
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
    // 枚举
    enumOrg(data) {
      debugger;
      let val = "";
      if (data == 1) {
        val = "重点发展区域镇办";
      } else if (data == 3) {
        val = "生态发展区域镇办";
      } else if (data == 4) {
        val = "党群部门";
      } else if (data == 5) {
        val = "政府经济部门";
      } else if (data == 6) {
        val = "政府非经济部门";
      } else if (data == 7) {
        val = "驻区单位";
      }
      return val;
    },
    btn1(val) {
      // console.log(val)
      this.list.push(val.srcElement.value);
    },
    btn2(val) {
      this.list.push(val.srcElement.value);
    },
    btn3(val) {
      this.list.push(val.srcElement.value);
    },
    tabBtn1(val) {
      console.log(this.map);
    },
    butt1(val) {
      debugger;
      this.list.push(val);
    },
    butt2(val) {
      debugger;
      this.list.push(val);
      console.log(this.list);
    },
    gxbtn() {
      this.$router.push("UnitList");
      // this.$axios({
      //   url:"url",
      //   method:"post",
      //   data:{CjList:this.list}
      // }).then((res)=>{
      //   console.log(res)
      // })
    },
    go() {
      this.$router.push("LeaderList");
    },
    ok() {
      this.$Message.info("Clicked ok");
    },
    cancel() {
      this.$Message.info("Clicked cancel");
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

.comCenter {
  width: 100%;
  height: 100%;
  display: -webkit-flex;
}
.cenLeft {
  width: 50%;
  height: 100%;
  padding: 30px 0 70px;
  border-right: 1px solid #d7d7d7;
  margin-top: 15px;
}
.cenRig {
  width: 50%;
  height: 100%;
  margin: 20px 30px;
}
.createBt {
  text-align: center;
}
.createBt .inputas .inputs {
  width: 50vh;
  line-height: 30px;
}
.inputas {
  margin-top: 40px;
  display: -webkit-flex;
  -webkit-justify-content: center;
}
.inputas span {
  margin-right: 20px;
  display: block;
}
.inputas p {
  font-size: 14px;
  margin-top: 10px;
  color: #a9a9a9;
}
.inputasrig {
  text-align: left;
}
.createZb {
  margin-top: 30px;
}
.createAn {
  margin-top: 50px;
  display: -webkit-flex;
  justify-content: center;
}
.createAn button {
  margin-right: 10px;
}
.createAns {
  font-size: 24px;
  color: black;
}
.cennum {
  width: 100%;
  height: 200px;
  line-height: 200px;
  display: -webkit-flex;
  margin-left: 30px;
}
.cennum span {
  color: black;
  font-size: 26px;
}
.cennum h3 {
  color: black;
  font-size: 36px;
  margin-left: 30px;
}
.cenMore {
  margin-top: 30px;
  text-align: right;
}
</style>
<style>
.createZb .ivu-tabs-bar {
  border-bottom: 0px solid #dcdee2;
}
.ivu-tabs-nav {
  float: none;
  font-size: 16px;
}
/*.nav-text ivu-tabs-nav .ivu-tabs-ink-bar{*/
/*left:1127px;*/
/*}*/
.ivu-radio-wrapper {
  font-size: 14px;
}
.ivu-tabs-nav-scroll {
  display: inline-block;
}
/*.ivu-btn-primary{*/
/*width: 130px;*/
/*}*/
.ivu-btn-large {
  width: 130px;
}
.ivu-btn > span {
}
.china-company .ivu-modal-body {
  height: 150px;
  line-height: 60px;
}
/*.ivu-input{*/
/*height: 50px;*/
/*}*/
</style>
