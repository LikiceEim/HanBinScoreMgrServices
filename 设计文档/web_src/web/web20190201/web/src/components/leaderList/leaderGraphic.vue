<template>
  <div class="craeteTop">
    <span style="display:inline-block;margin-left:10px;color:#000;font-size:23px;">干部积分排名</span>
    <div class="list-red" ref="listRed"></div>
  </div>
</template>

<script>
import {  secondLevelGetLeaderList} from "@/api/leaderList";
import Cookies from 'js-cookie';

export default {
  name: "LeaderGraphic",
  created: {},
  created: function(){
    this.drawLine();
  },
  methods: {
    //绘制图形
    // 画图
    drawLine() {
      debugger;
      // 红榜
      var OrganizationID =-1;// 单位类型ID
          var LevelID = -1;// 级别
          var Keyword = null;// 关键词
          var Page = 1;// 页码
          var PageSize = 1000;// 页数
          var Sort = '';
          var Order = '';
          var CurrentUserID = Cookies.get('UserID');
          var _data = {
            OrganizationID,
            LevelID,
            Keyword,
            Page,
            PageSize,
            Sort,
            Order,
            CurrentUserID
          }
      secondLevelGetLeaderList(_data).then(res => {

        debugger;
        var rowData = res.Result.OfficerInfoItemList;
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
                data: yData,
                type: "bar",
                 itemStyle: {
                  normal: {
                    label: {
                      show: true, //开启显示
                      position: 'top', //在上方显示
                      textStyle: { //数值样式
                        color: 'black',
                        fontSize: 12
                      }
                    }
                  }
                },
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
          });
        } else {
          this.$Message.error(res.Reason);
        }
      });
    }
  }
};
</script>

<style scoped>
  .craeteTop{
    width:100%;
    height:65px;
    font-size: 24px;
    font-weight: 600;
    background: #cdd0d4;
    line-height: 65px;
    padding-left: 60px;
    color: #323232;
    font-size: 25px;
  }

</style>
