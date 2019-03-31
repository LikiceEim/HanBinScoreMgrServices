/*
 * @Author: Willynn 
 * @Date: 2018-12-15 15:58:28 
 * @Last Modified by: mikey.zhaopeng
 * @Last Modified time: 2019-01-29 22:05:30
 */

 <template>
  <div class="dimensional-pre">
    <div class="craeteTop header">
      <div class="name">
        分维度展示
      </div>
    </div>
    <div class="average-area">
      <div class="title">区域平均分</div>
      <div style="margin-top:10px;margin-left:20px;">
        <!-- <RadioGroup v-model="areaSelect" type="button" @on-change="changeAreaToDraw">
          <Radio label="街道、镇"></Radio>
          <Radio label="乡"></Radio>
        </RadioGroup> -->
        <span>单位类型平均分</span>
      </div>
      <div ref="areAverage" class="area-chart"></div>
    </div>
    <div class="average-score">
      <div class="title">年龄平均分</div>
      <div style="margin-top:10px;margin-left:20px;">
        <span>年龄范围</span>
        <!-- <Select v-model="startAge" style="width:200px">
          <Option v-for="item in ageList" :value="item.value" :key="item.value">{{ item.label }}</Option>
        </Select> -->
        <DatePicker class="yearsYear" v-model="startAge" type="year" placeholder="请选择开始时间" style="width:200px"></DatePicker>
        <span>—</span>
        <!-- <Select v-model="endAge" style="width:200px">
          <Option v-for="item in ageList" :value="item.value" :key="item.value">{{ item.label }}</Option>
        </Select> -->
        <DatePicker class="yearsYear" v-model="endAge" type="year" placeholder="请选择结束时间" style="width:200px"></DatePicker>
        <span>工龄</span>
        <Select v-model="ageDifference" style="width:200px">
          <Option v-for="item in ageDifferenceList" :value="item.value" :key="item.value">{{ item.label }}</Option>
        </Select>
        <Button class="primary" type="primary" @click="DrawAgeAverageChart">搜索</Button>
      </div>
      <div ref="ageAverage" class="area-chart"></div>
    </div>
    <div class="average-type">
      <div class="title">单位积分</div>
      <div style="margin-top:10px;margin-left:20px;">
        <span>单位分类</span>
        <Select v-model="unitType" style="width:200px">
          <Option v-for="item in unitTypeArr" :value="item.value" :key="item.value">{{ item.label }}</Option>
        </Select>
        <span>职位级别</span>
        <Select v-model="positionType" style="width:200px">
          <Option v-for="item in LevelTypeArr" :value="item.value" :key="item.value">{{ item.label }}</Option>
        </Select>
        <span>工龄</span>
        <Select v-model="workingAge" style="width:200px">
          <Option v-for="item in workingAgeArr" :value="item.value" :key="item.value">{{ item.label }}</Option>
        </Select>
        <Button class="primary" type="primary" @click="DrawTypeAverageChart">搜索</Button>
      </div>
      <div ref="typeAverage" class="area-chart"></div>
    </div>
  </div>
 </template>
 
 <script>
 import {queryAreaAverageScore, queryAgeAverageScore, queryOrganAverageScore,queryOrganCategoryAverageScore} from '@/api/dimensionalPre';
 import {queryUnitName} from '@/api/usersManage';
  import {queryLevelList} from '@/api/leaderList';
 export default {
   name: 'dimensionalPre',
   data() {
     return{
       // 区域平均分数据
       areAverageData: {},
       // 年龄平均分数据
       ageAverageData: {},
       // 类型积分数据
       typeAverageData: {},
       // 设置单选的值
       areaSelect: '街道、镇',
      // 年龄范围开始
      startAge: null,
      // 年龄范围结束
      endAge: null,
      // 年龄范围List
      ageList: [],
      // 平均年限差
      ageDifference: null,
      // 平均年限差List
      ageDifferenceList: [],
      // 单位分类
      unitType: null,
      // 单位分类List
      unitTypeArr:[],
      // 职位分类
      positionType: null,
      // 职位分类list
      LevelTypeArr: [],
      // 工龄
      workingAge: null,
      // 工龄list
      workingAgeArr: [],

      //单位类型平均分数据， added by QXM
      organCategoryAverageData:{}
     }
   },
   props:['echartsInfo'],
   watch:{
     echartsInfo: {
       handler(newVal, oldVal) {
         debugger;

       },
       deep: true
     }
   },
   created() {
     debugger;
     // 构造年龄平均年限差
     var ageList = [{
       label: '全部',
       value: null
     }];
     var index = 1;
     for(let i = 0; i < 30; i++) {
       var tempObj = {};
       tempObj.label = index + '年';
       tempObj.value = index;
       index = index + 1;
       ageList.push(tempObj);
     }
     this.ageDifferenceList = ageList;
     this.workingAgeArr = ageList;
     // 获取单位分类
      queryUnitName().then(res => {
        debugger;
        if(res.IsSuccessful == true) {
          this.unitTypeArr = [];
          var Option = [];
          Option.push({
             label: '全部',
            value: null
          })
          for(let i = 0; i < res.Result.OrganList.length; i++) {
            var tempObj = {};
            tempObj.label = res.Result.OrganList[i].OrganFullName;
            tempObj.value = res.Result.OrganList[i].OrganID;
            Option.push(tempObj);
          }
          this.unitTypeArr = Option;
        } else {
          this.$Message.error(res.Reason);
        }
      });
      // 获取职位
      queryLevelList().then(res => {
        debugger;
        if(res.IsSuccessful == true) {
          this.LevelTypeArr = [];
          var row = res.Result.LevelList;
          var list = [];
          list.push({
             label: '全部',
             value: null
          })
          for(let i = 0; i < row.length; i++) {
            var tempObj = {};
            tempObj.label = row[i].LevelName;
            tempObj.value = row[i].LevelID;
            list.push(tempObj);
          }
          this.LevelTypeArr = list;
        }else{
          this.$Message.error(res.Reason);
        }
      });
   },
   mounted() {
     this.Draw();
   },
   methods: {
     // 单选框绘制区域平均分图
     changeAreaToDraw(val) {
       debugger;
       this.areaSelect = val;
       if(val == '街道、镇') {
         // 请求接口，绘制数据

       } else if(val == '乡') {
         // 请求接口，绘制数据
         
       }
     },
     // 绘制年龄平均分图
     DrawAgeAverageChart(){
       debugger;
       var BirthdayFrom = this.startAge;
       var BirthdayTo = this.endAge;
       var WorkYears = this.ageDifference;
       var _data = {
         BirthdayFrom,
         BirthdayTo,
         WorkYears
       }
       queryAgeAverageScore(_data).then(res => {
         debugger;
         // #d2d486
         if(res.IsSuccessful == true){
           var data = res.Result.AgeAverageScoreItemList;
            var xData = [],yData = [];
            for(let i = 0; i < data.length; i++){
              xData.push(data[i].Year);
              var tempObj = {
                value: data[i].AverageScore,
                itemStyle:{
                  color:'#d2d486'
                }
              }
              yData.push(tempObj);
            }
            var ageAverageOption = {
              tooltip: {
                show:true
              },
              xAxis: {
                type: 'category',
                axisLabel:{
                    interval: 0,
                    textStyle:{
                      fontSize:12
                    },
                  },
                data: xData
              },
              yAxis: {
                type: 'value'
              },
              series: [{
                data: yData,
                type: 'bar'
              }]
            };
            var ageEcharts = this.$echarts.init(this.$refs.ageAverage);
            ageEcharts.setOption(ageAverageOption);
         }else{
           this.$Message.error(res.Reason);
         }
       })
     },
     // 绘制类型积分
     DrawTypeAverageChart(){
       debugger;
        var OrganID = this.unitType;
        var LevelID = this.positionType;
        var WorkYears = this.workingAge;
        var _data = {
          OrganID,
          LevelID,
          WorkYears
        }
        queryOrganAverageScore(_data).then(res => {
          debugger;
          // #4bb9f6
          if(res.IsSuccessful == true) {
            var data = res.Result.OrganAverageScoreItemList;
            var xData = [],yData = [];
            for(let i = 0; i < data.length; i++){
              xData.push(data[i].Name);
              var tempObj = {
                value: data[i].CurrentScore,
                itemStyle:{
                  color:'#4bb9f6'
                }
              }
              yData.push(tempObj);
            }
            var typeAverageOption = {
              tooltip: {
                show:true
              },
              xAxis: {
                type: 'category',
                axisLabel:{
                    interval: 0,
                    textStyle:{
                      fontSize:12
                    },
                  },
                data: xData
              },
              yAxis: {
                type: 'value'
              },
              series: [{
                data: yData,
                type: 'bar'
              }]
            };
            var typeEcharts = this.$echarts.init(this.$refs.typeAverage);
            typeEcharts.setOption(typeAverageOption);
          }else{
            this.$Message.error(res.Reason);
          }
        })
     },
     // 画图
     Draw() {
       debugger;

       /* 绘制单位大类平均分， added by QXM  */
       queryOrganCategoryAverageScore().then(res => {
          debugger;
          // 绘制区域平均分图
          // #00c9dd
          if(res.IsSuccessful == true) {
            var data = res.Result.OrganCategoryAverageScoreItemList;
            var xData = [],yData = [];
            for(let i = 0; i < data.length; i++){
              xData.push(data[i].OrganCategoryName);
              var tempObj = {
                value: data[i].AverageScore,
                itemStyle:{
                  color:'#00c9dd'
                }
              }
              yData.push(tempObj);
            }
            var areAverageOption = {
              tooltip: {
                show:true
              },
              xAxis: {
                type: 'category',
                axisLabel:{
                    interval: 0,
                    textStyle:{
                      fontSize:12
                    },
                  },
                data:xData
              },
              yAxis: {
                type: 'value'
              },
              series: [{
                // data: [120, 200, 150, 80, 70, 110, 130],
                data:yData,
                type: 'bar'
              }]
            };
            var areaEcharts = this.$echarts.init(this.$refs.areAverage);
            areaEcharts.setOption(areAverageOption);
          }else{
            this.$Message.error(res.Reason);
          }
       })
        
        // 绘制年龄平均分图
        var BirthdayFrom = this.startAge;
       var BirthdayTo = this.BirthdayTo;
       var WorkYears = this.ageDifference;
       var _data = {
         BirthdayFrom,
         BirthdayTo,
         WorkYears
       }
       queryAgeAverageScore(_data).then(res => {
         debugger;
         // #d2d486
         if(res.IsSuccessful == true){
           var data = res.Result.AgeAverageScoreItemList;
            var xData = [],yData = [];
            for(let i = 0; i < data.length; i++){
              xData.push(data[i].Year);
              var tempObj = {
                value: data[i].AverageScore,
                itemStyle:{
                  color:'#d2d486'
                }
              }
              yData.push(tempObj);
            }
            var ageAverageOption = {
              tooltip: {
                show:true
              },
              xAxis: {
                type: 'category',
                axisLabel:{
                    interval: 0,
                    textStyle:{
                      fontSize:12
                    },
                  },
                data: xData
              },
              yAxis: {
                type: 'value'
              },
              series: [{
                data: yData,
                type: 'bar'
              }]
            };
            var ageEcharts = this.$echarts.init(this.$refs.ageAverage);
            ageEcharts.setOption(ageAverageOption);
         }else{
           this.$Message.error(res.Reason);
         }
       })
        // 绘制类型积分
        var OrganID = this.unitType;
        var LevelID = this.positionType;
        var WorkYears = this.workingAge;
        var _data = {
          OrganID,
          LevelID,
          WorkYears
        }
        queryOrganAverageScore(_data).then(res => {
          debugger;
          // #4bb9f6
          if(res.IsSuccessful == true) {
            var data = res.Result.OrganAverageScoreItemList;
            var xData = [],yData = [];
            for(let i = 0; i < data.length; i++){
              xData.push(data[i].Name);
              var tempObj = {
                value: data[i].CurrentScore,
                itemStyle:{
                  color:'#4bb9f6'
                }
              }
              yData.push(tempObj);
            }
            var typeAverageOption = {
              tooltip: {
                show:true
              },
              xAxis: {
                type: 'category',
                axisLabel:{
                    interval: 0,
                    textStyle:{
                      fontSize:12
                    },
                  },
                data: xData
              },
              yAxis: {
                type: 'value'
              },
              series: [{
                data: yData,
                type: 'bar'
              }]
            };
            var typeEcharts = this.$echarts.init(this.$refs.typeAverage);
            typeEcharts.setOption(typeAverageOption);
          }else{
            this.$Message.error(res.Reason);
          }
        })
     }
   }
 }
 </script>
 
 <style scoped>
 /* .dimensional-pre .header{
    width: 100%;
    height: 78px;
    font-size: 24px;
    text-align: left;
 }
 .dimensional-pre .header .name{
    width: 100%;
    height: 78px;
    background: inherit;
    background-color: rgba(247, 247, 249, 1);
    border: none;
    border-radius: 2px;
    -moz-box-shadow: none;
    -webkit-box-shadow: none;
    box-shadow: none;
    font-size: 24px;
    text-align: left;
 } */
 .craeteTop{
    width:100%;
    height:65px;
    font-size: 24px;
    font-weight: 600;
    background: #cdd0d4;
    line-height: 65px;
    padding-left: 60px;
    color: #323232;
  }
  .dimensional-pre .average-area{
    width: 100%;
    /* height: 40px; */
    background: inherit;
    
    border: none;
    border-radius: 2px;
    border-bottom-right-radius: 0px;
    border-bottom-left-radius: 0px;
    -moz-box-shadow: none;
    -webkit-box-shadow: none;
    box-shadow: none;
    font-size: 14px;
    text-align: left;
    position: relative;
  }
  .dimensional-pre .average-score{
    position: relative;
  }
  .dimensional-pre .average-type{
    position: relative;
  }
  .dimensional-pre .title{
    background-color: rgba(191, 194, 205, 1);
    line-height: 40px;
    height: 40px;
    vertical-align: middle;
    padding-left: 30px;
  }
  .dimensional-pre .area-chart{
    width: 100%;
    height: 300px;
  }
 </style>
 