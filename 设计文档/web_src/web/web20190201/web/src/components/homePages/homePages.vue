<template>
  <div>
    <div class="homeTop">
      <div class="homeDiv">
        <!-- <img src="../../assets/image/home1.gif" alt=""> -->
        <div class="homeInnerDiv" style="background-color:#29c6c4;">
          <img src="/static/image/unitlogo.png" alt>
        </div>
        <div class="homes">
          <h4>{{unit}}</h4>
          <span>单位</span>
        </div>
      </div>
      <div class="homeDiv" style="padding-left:20px;">
        <div class="homeInnerDiv" style="background-color:#ff564d;">
          <img src="/static/image/leaderlogo.png" alt>
        </div>
        <div class="homes">
          <h4>{{cadres}}</h4>
          <span>干部</span>
        </div>
      </div>
      <div class="homeDiv" style="padding-left:20px;">
        <div class="homeInnerDiv" style="background-color:#ffcb49;">
          <img src="/static/image/userlogo.png" alt>
        </div>
        <div class="homes">
          <h4>{{users}}</h4>
          <span>用户</span>
        </div>
      </div>
      <div class="homeDiv" style="padding-left:20px;">
        <div class="homeInnerDiv" style="background-color:#00c4f0;">
          <img src="/static/image/averagelogo.png" alt>
        </div>
        <div class="homes">
          <h4>{{average}}</h4>
          <span>平均分</span>
        </div>
      </div>
    </div>

    <!-- <div class="homeBot">
      <div class="homeBot-lf">

      </div>
      <div class="homeBot-ri">
        <div class="honeBot-ech">
          <div class="honeBot-ech-one">
            fdfd
          </div>
          <div id="myChart" class="honeBot-ech-one">
          </div>
          <div class="honeBot-ech-one">
            fdf
          </div>
        </div>
      </div>
    </div>-->
    <div class="list-all">
      <div class="list-half">
        <span style="display:inline-block;margin-left:20px;color:#000;font-size:15px;">红榜</span>
        <div class="list-red" ref="listRed"></div>
      </div>
      <div class="list-half" style="margin-left:50px;">
        <span style="display:inline-block;margin-left:20px;color:#000;font-size:15px;">黄榜</span>
        <div class="list-black" ref="listBlack"></div>
      </div>
    </div>

    <div class="list-center" style="height:auto;">
      <div class="list-half" style="height:auto;">
        <div style="display:inline-block;margin-left:20px;color:#000;font-size:15px;width:100%;">
          <span>待办事项</span>
          <span
            class="list-center-more"
            style="cursor:pointer;"
            @click="handleClickToDoList(1)"
          >更多>></span>
        </div>
        <div class="list-content">
          <div class="list-content-div" v-show="isToDoListShow" :style="{width:isRole}">
            <span
              style="font-size:14px;font-weight:bold;display:inline-block;margin-left:10px;width:100%;"
            >待我审批</span>
            <template v-for="(item, index) in ToDoList">
              <span
                :key="index"
                @click="handleClickToDoList(1)"
                style="width:100%;display:inline-block;padding-left:10px;display:flex;align-items: center;cursor:pointer;"
              >
                <span
                  style="display:inline-block;width:8px;height:8px;border-radius:4px;background-color:#00c4f0;"
                ></span>
                <span class="item-content-applytitle" :title="item.ApplyTitle">{{item.ApplyTitle}}</span>
                <span style="color:#ccc;width:80px;font-size:14px;">{{item.ApplyDate}}</span>
              </span>
            </template>
          </div>
          <div class="list-content-div" v-show="isScoreChangeShow" :style="{width:isRole}">
            <span
              style="font-size:14px;font-weight:bold;display:inline-block;margin-left:10px;width:100%;"
            >上级反馈</span>
            <template v-for="(item, index) in SuperiorFeedbackList">
              <span
                :key="index"
                @click="handleClickToDoList(2)"
                style="width:100%;display:inline-block;padding-left:10px;display:flex;align-items: center;cursor:pointer;"
              >
                <span
                  style="display:inline-block;width:8px;height:8px;border-radius:4px;background-color:#00c4f0;"
                ></span>
                <span
                  class="item-content-applytitle"
                  :title="item.FeedBackTitle"
                >{{item.FeedBackTitle}}</span>
                <span style="color:#ccc;width:86px;font-size:14px;">{{item.LastUpdateDate}}</span>
              </span>
            </template>
          </div>
        </div>
      </div>
      <div class="list-half" style="margin-left:50px;height:auto;">
        <span style="display:inline-block;margin-left:20px;color:#000;font-size:15px;">变更公示</span>
        <span class="list-center-more" style="cursor:pointer;" @click="handleClickToDoList(3)">更多>></span>
        <div class="list-content" style="width:100%;background-color:#fff;flex-direction:column;">
          <template v-for="(item, index) in ScoreChangeHistory">
            <span :key="index" style="display:inline-block;width:100%;">
              <span
                @click="handleClickToDoList(3)"
                style="width:100%;display:inline-block;padding-left:10px;display:flex;align-items: center;cursor:pointer;"
              >
                <span v-if="item.ItemScore<0" style="color:#ccc;">{{item.ItemScore}}</span>
                <span v-else-if="item.ItemScore>=0" style="color:#00b576;">+{{item.ItemScore}}</span>
                <span :title="item.Content" class="item-content-Content">{{item.Content}}</span>
                <span style="color:#ccc;width:120px;font-size:14px;">{{item.AddDate}}</span>
              </span>
            </span>
          </template>
        </div>
      </div>
    </div>

    <div class="list-footer">
      <div class="list-footer-title">
        <div style="display:inline-block;margin-left:20px;color:#000;font-size:15px;width:100%;">
          <span>积分公示</span>
          <span
            class="list-center-more"
            style="cursor:pointer;"
            @click="handleClickToDoList(4)"
          >更多>></span>
        </div>
      </div>
      <div>
        <i-table :columns="PublicScore" :data="PublicScoreData"></i-table>
      </div>
    </div>
  </div>
</template>

<script>
import {
  queryAllPageList,
  queryRedBoardData,
  queryBlackBoardData,
  queryToDoList,
  querySuperiorFeedbackList,
  queryScoreChangeHistory,
  queryScorePublicShow
} from "@/api/homePages";
import Cookies from "js-cookie";
export default {
  name: "homePages",
  data() {
    return {
      isToDoListShow: false,
      isScoreChangeShow: false,
      isRole: "50%",
      theme1: "light",
      //头部数据
      // 单位
      unit: 0,
      // 干部
      cadres: 0,
      // 用户
      users: 0,
      // 平均分
      average: 0,
      // 榜单个数
      RankNumber: 10,
      // 待我审批列表
      ToDoList: [],
      // 上级反馈列表
      SuperiorFeedbackList: [],
      // 变更公示列表
      ScoreChangeHistory: [],
      // 积分公示列
      PublicScore: [
        {
          title: "积分排名",
          key: "CurrentScore"
        },
        {
          title: "排名",
          key: "Rank"
        },
        {
          title: "姓名",
          key: "Name"
        },
        {
          title: "性别",
          key: "Gender",
          render: (h, params) => {
            return h("span", this.formatSex(params.row.Gender));
          }
        },
        {
          title: "出生日期",
          key: "Birthday"
        },
        {
          title: "所在单位",
          key: "OrganFullName"
        },
        {
          title: "现任职务",
          key: "PositionName"
        },
        {
          title: "级别",
          key: "LevelName"
        },
        {
          title: "任职时间",
          key: "OnOfficeDate"
        }
      ],
      // 积分公示数据
      PublicScoreData: [],
      PublicScorePage: 1,
      PublicScorePageSize: 10
    };
  },
  created() {
    debugger;
    var roleID = Cookies.get("RoleID");
    if (roleID == 1) {
      this.isToDoListShow = true;
      this.isScoreChangeShow = true;
      this.isRole = "50%";
    } else if (roleID == 3) {
      this.isToDoListShow = true;
      this.isScoreChangeShow = false;
      this.isRole = "100%";
    } else if (roleID == 4) {
      this.isToDoListShow = false;
      this.isScoreChangeShow = true;
      this.isRole = "100%";
    }
    debugger;
    // 获取头部信息
    this.getHeaderData();
    // 获取待办事项
    this.getToDoList();
    // 获取上级反馈列表
    this.getSuperiorFeedbackList();
    // 获取变更公示列表
    this.getScoreChangeHistory();
    // 获取积分公示列表
    this.getScorePublicShow();
  },
  mounted() {
    // 画图
    this.drawLine();
  },
  methods: {
    // 格式化
    formatSex(data) {
      debugger;
      if (data == 1) {
        return "男";
      } else if (data == 2) {
        return "女";
      }
    },
    // 待办事项列表
    getToDoList() {
      debugger;
      var _data = {
        RankNumber: this.RankNumber
      };
      queryToDoList(_data).then(res => {
        debugger;
        if (res.IsSuccessful == true) {
          if (res.Result == null) {
            this.ToDoList = [];
            return false;
          }
          this.ToDoList = res.Result.WhatToDoList;
          var index = 1;
          var list = [];
          if (res.Result.WhatToDoList.length < 10) {
            for (let i = 0; i < res.Result.WhatToDoList.length; i++) {
              var tempObj = {};
              tempObj.ApplyID = index;
              index = index + 1;
              // tempObj.ApplyTitle = '区民政府管理员提交了一个申请';
              tempObj.ApplyTitle = res.Result.WhatToDoList[i].ApplyTitle;
              // tempObj.ApplyDate = '2018-02-05';
              var ApplyDate = res.Result.WhatToDoList[i].ApplyDate;
              ApplyDate = ApplyDate.split(" ")[0];
              tempObj.ApplyDate = ApplyDate;
              list.push(tempObj);
            }
          } else {
            for (let i = 0; i < 9; i++) {
              var tempObj = {};
              tempObj.ApplyID = index;
              index = index + 1;
              // tempObj.ApplyTitle = '区民政府管理员提交了一个申请';
              tempObj.ApplyTitle = res.Result.WhatToDoList[i].ApplyTitle;
              // tempObj.ApplyDate = '2018-02-05';
              var ApplyDate = res.Result.WhatToDoList[i].ApplyDate;
              ApplyDate = ApplyDate.split(" ")[0];
              tempObj.ApplyDate = ApplyDate;
              list.push(tempObj);
            }
          }
          this.ToDoList = list;
        } else {
          this.$Message.error(res.Reason);
        }
      });
    },
    // 上级反馈列表
    getSuperiorFeedbackList() {
      debugger;
      var CurrentUserID = Cookies.get("UserID");
      var _data = {
        RankNumber: this.RankNumber,
        CurrentUserID: CurrentUserID
      };
      querySuperiorFeedbackList(_data).then(res => {
        debugger;
        if (res.IsSuccessful == true) {
          if (res.Result == null) {
            this.SuperiorFeedbackList = null;
            return false;
          }
          this.SuperiorFeedbackList = res.Result.FeedBackList;
          var index = 1;
          var list = [];
          if (res.Result.FeedBackList.length < 10) {
            for (let i = 0; i < res.Result.FeedBackList.length; i++) {
              var tempObj = {};
              tempObj.ApplyID = index;
              index = index + 1;
              tempObj.FeedBackTitle = res.Result.FeedBackList[i].FeedBackTitle;
              var LastUpdateDate = res.Result.FeedBackList[i].LastUpdateDate;
              LastUpdateDate = LastUpdateDate.split(" ")[0];
              tempObj.LastUpdateDate = LastUpdateDate;
              list.push(tempObj);
            }
          } else {
            for (let i = 0; i < 9; i++) {
              var tempObj = {};
              tempObj.ApplyID = index;
              index = index + 1;
              tempObj.FeedBackTitle = res.Result.FeedBackList[i].FeedBackTitle;
              var LastUpdateDate = res.Result.FeedBackList[i].LastUpdateDate;
              LastUpdateDate = LastUpdateDate.split(" ")[0];
              tempObj.LastUpdateDate = LastUpdateDate;
              list.push(tempObj);
            }
          }
          this.SuperiorFeedbackList = list;
        } else {
          this.$Message.error(res.Reason);
        }
      });
    },
    // 获取变更公式列表
    getScoreChangeHistory() {
      debugger;
      var CurrentUserID = Cookies.get("UserID");
      var _data = {
        RankNumber: this.RankNumber,
        CurrentUserID: CurrentUserID
      };
      queryScoreChangeHistory(_data).then(res => {
        debugger;
        if (res.IsSuccessful == true) {
          if (res.Result == null) {
            this.ScoreChangeHistory = [];
            return false;
          }
          var row = res.Result.ScoreChangeHisList;
          var list = [];
          for (let i = 0; i < row.length; i++) {
            var tempObj = {};
            tempObj.ItemScore = row[i].ItemScore;
            tempObj.Content = row[i].Content;
            // tempObj.Content = '区招商局 张某某 荣获市级荣誉称号';
            var date = row[i].AddDate.split(" ")[0];
            tempObj.AddDate = date;
            list.push(tempObj);
          }
          this.ScoreChangeHistory = list;
        } else {
          this.$Message.error(res.Reason);
        }
      });
    },
    // 获取积分公示列表
    getScorePublicShow() {
      debugger;
      var CurrentUserID = Cookies.get("UserID");
      var _data = {
        Page: this.PublicScorePage,
        PageSize: this.PublicScorePageSize,
        CurrentUserID: CurrentUserID
      };
      queryScorePublicShow(_data).then(res => {
        debugger;
        if (res.IsSuccessful == true) {
          if (res.Result == null) {
            this.PublicScoreData = [];
            return false;
          }
          var row = res.Result.OfficerScoreShowList;
          for (let i = 0; i < row.length; i++) {
            var time = row[i].Birthday;
            time = time.split(" ")[0];
            var officeTime = row[i].OnOfficeDate;
            officeTime = officeTime.split(" ")[0];
            row[i].Birthday = time;
            row[i].OnOfficeDate = officeTime;
          }
          this.PublicScoreData = row;
        } else {
          this.$Message.error(res.Reason);
        }
      });
    },
    // 获取头部信息
    getHeaderData() {
      debugger;
      queryAllPageList().then(res => {
        debugger;
        if (res.IsSuccessful == true) {
          this.unit = res.Result.OrganizatonCount;
          this.cadres = res.Result.OfficerCount;
          this.users = res.Result.UserCount;
          this.average = res.Result.AvarageScore.toFixed(1);
        } else {
          this.$Message.error(res.Reason);
        }
      });
    },
    // 画图
    drawLine() {
      // 红榜
      var CurrentUserID = Cookies.get("UserID");
      var _data = {
        RankNumber: this.RankNumber,
        CurrentUserID: CurrentUserID
      };
      queryRedBoardData(_data).then(res => {
        var rowData = res.Result.RankList;
        var xData = [],
          yData = [];
        var officerIDDict = []; //存储柱状图index 与 OfficerID的对应关系
        for (let i = 0; i < rowData.length; i++) {
          var offDic = { index: i, OfficerID: rowData[i].OfficerID };
          officerIDDict.push(offDic);

          xData.push(rowData[i].OfficerName);
          if (i % 2 == 1) {
            var tempObj = {
              value: rowData[i].CurrentScore,
              itemStyle: {
                color: "#ff6268"
              }
            };
          } else {
            var tempObj = {
              value: rowData[i].CurrentScore,
              itemStyle: {
                color: "#ff6268"
              }
            };
          }
          yData.push(tempObj);
        }
        if (res.IsSuccessful == true) {
          var redListOption = {
            tooltip: {
              show: true
            },
            xAxis: {
              type: "category",
              data: xData,
              axisLabel: {
                interval: 0,
                textStyle: {
                  fontSize: 12
                },
                rotate: 40
              }
            },
            yAxis: {
              type: "value"
            },
            series: [
              {
                // itemStyle:{
                //   normal: {
                //     color:function(params) {
                //       var colorList = [
                //         '#C1232B','#B5C334','#FCCE10','#E87C25','#27727B',
                //           '#FE8463','#9BCA63','#FAD860','#F3A43B','#60C0DD',
                //           '#D7504B','#C6E579','#F4E001','#F0805A','#26C0C0'
                //       ];
                //       return colorList[params.dataIndex]
                //     }
                //   }
                // },
                data: yData,
                type: "bar"
              }
            ]
          };
          var redEcharts = this.$echarts.init(this.$refs.listRed);
          redEcharts.setOption(redListOption);

          var that = this;
          redEcharts.on("click", function(param) {
            console.log(param);
            console.log("点击了红榜数据");
            debugger;
            var indx = param.dataIndex;
            for (var i = 0; i < officerIDDict.length; i++) {
              if (officerIDDict[i].index == indx) {
                var officerID = officerIDDict[i].OfficerID;

                var data = {};
                data.OfficerID = officerID;

                debugger;

                  that.$router.push({ path: "LeaPerInfor", query: data }); //路由跳转至干部详情页
              }
            }

            //this.$router.push({path:"LeaPerInfor", query:data}) //路由跳转至干部详情页
          });
        } else {
          this.$Message.error(res.Reason);
        }
      });
      // 黑榜
      queryBlackBoardData(_data).then(res => {
        var rowData = res.Result.RankList;
        var xData = [],
          yData = [];

        var officerIDDict = []; //存储柱状图index 与 OfficerID的对应关系
        for (let i = 0; i < rowData.length; i++) {
          var offDic = { index: i, OfficerID: rowData[i].OfficerID };
          officerIDDict.push(offDic);

          xData.push(rowData[i].OfficerName);
          var temp = {
            value: rowData[i].CurrentScore,
            itemStyle: {
              color: "#cccccc"
            }
          };
          // yData.push(rowData[i].CurrentScore);
          yData.push(temp);
        }
        var blackListOption = {
          tooltip: {
            show: true
          },
          xAxis: {
            type: "category",
            data: xData,
            axisLabel: {
              interval: 0,
              textStyle: {
                fontSize: 12
              },
              rotate: 40
            }
          },
          yAxis: {
            type: "value"
          },
          series: [
            {
              // itemStyle:{
              //   normal: {
              //     color:function(params) {
              //       var colorList = [
              //         '#cccccc'
              //       ];
              //       return colorList[params.dataIndex]
              //     }
              //   }
              // },
              data: yData,
              type: "bar"
            }
          ]
        };
        var blackEcharts = this.$echarts.init(this.$refs.listBlack);
        blackEcharts.setOption(blackListOption);

        var that = this;
        blackEcharts.on("click", function(param) {
          console.log(param);
          console.log("点击了黑榜数据");
          debugger;
          var indx = param.dataIndex;
          for (var i = 0; i < officerIDDict.length; i++) {
            if (officerIDDict[i].index == indx) {
              var officerID = officerIDDict[i].OfficerID;

              var data = {};
              data.OfficerID = officerID;

              that.$router.push({ path: "Mains/LeaPerInfor", query: data }); //路由跳转至干部详情页
              return;
            }
          }

          //this.$router.push({path:"LeaPerInfor", query:data}) //路由跳转至干部详情页
        });
      });
    },
    // 待办事项跳转
    handleClickToDoList(type) {
      debugger;
      this.$router.push({ name: "approval", query: { type: type } });
    }
  }
  //   methods:{
  //     drawLine(){
  //       // 基于准备好的dom，初始化echarts实例
  //       let myChart = this.$echarts.init(document.getElementById('myChart'));
  //       myChart.setOption({
  //         title: { text: '项目计量统计' },
  //         legend:{ name:"住宅", name:"办公"},
  //         xAxis: {
  //           type: 'category',
  //           data: ['12/07', '12/08', '12/09', '12/10', '12/11'],
  //           axisTick:{
  //             show:false,
  //           }
  //
  //         },
  //         yAxis: {
  //           type: 'value'
  //         },
  //         series: [{
  //           name:"住宅",
  //           data: [820, 932, 901, 934, 1290],
  //           type: 'line',
  //           smooth: true
  //         },{
  //           name:"办公",
  //           data: [932, 1032, 661, 1234, 1090],
  //           type: 'line',
  //           smooth: true
  //         }]
  //       })
  //     }
  //   }
  //     // methods:{
  //   //   btn1(){
  //   //     // debugger;
  //   //       console.log(this.value1);
  //   //   }
  //   // }
};
</script>

<style scoped>
.homeTop {
  width: 100%;
  height: 125px;
  background: #eff0f6;
  display: -webkit-flex;
  -webkit-justify-content: space-between;
  -webkit-align-items: center;
}
.homeDiv {
  display: -webkit-flex;
  width: 25%;
}
.homeDiv img {
  /* width: 170px; */
  width: 35px;
  /* height: 98px; */
  height: 34px;
  display: block;
}
.homeInnerDiv {
  width: 170px;
  height: 98px;
  display: flex;
  justify-content: center;
  align-items: center;
}
.homes {
  display: -webkit-flex;
  width: 170px;
  height: 98px;
  flex-direction: column;
  -webkit-justify-content: center;
  -webkit-align-items: center;
  background: #fff;
}
.homes h4 {
  font-size: 40px;
}
.homes span {
  font-size: 18px;
  display: block;
}
.homeBot {
  width: 100%;
  height: 100%;
  background: aqua;
  display: -webkit-flex;
}
.homeBot-lf {
  width: 35%;
  height: 500px;
  background: antiquewhite;
}
.homeBot-ri {
  width: 65%;
  height: 500px;
  background: skyblue;
}
.honeBot-ech {
  height: 40%;
  width: 100%;
  background: aliceblue;
  display: -webkit-flex;
}
.honeBot-ech-one {
  height: 100%;
  border: 1px solid red;
  width: 33.3%;
}
.list-all {
  display: flex;
  width: 100%;
  background: #eff0f6;
  height: 340px;
}
.list-half {
  /* flex: 0.5; */
  width: 50%;
  justify-content: space-between;
  background-color: rgba(191, 194, 205, 1);
  height: 40px;
  line-height: 40px;
}
.list-red {
  width: 100%;
  justify-content: space-between;
  height: 300px;
  background-color: #fff;
}
.list-black {
  width: 100%;
  justify-content: space-between;
  height: 300px;
  background-color: #fff;
}
.list-content {
  display: flex;
  width: 100%;
  /* height: 100%; */
}
.list-content-div {
  width: 50%;
  background-color: #fff;
}
.list-center {
  display: flex;
  width: 100%;
  background: #eff0f6;
  height: 340px;
  padding-top: 20px;
}
.list-center-more {
  display: inline-block;
  float: right;
  margin-right: 30px;
  font-size: 12px;
  color: #333333;
}

.list-footer {
  display: flex;
  width: 100%;
  background: #eff0f6;
  /* height: 340px; */
  padding-top: 20px;
  flex-direction: column;
}
.list-footer-title {
  background-color: rgba(191, 194, 205, 1);
  height: 40px;
  line-height: 40px;
  width: 100%;
  padding-left: 20px;
  color: #000;
  font-size: 15px;
}
.item-content-applytitle {
  width: calc(100% - 90px);
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
  display: inline-block;
  margin-left: 5px;
}
.item-content-Content {
  width: calc(100% - 100px);
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
  display: inline-block;
  margin-left: 5px;
}
</style>
