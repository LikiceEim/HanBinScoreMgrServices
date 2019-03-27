<template>
  <div class="personal">
    <div class="centers">
      <div class="personals">
        <span> 积分情况</span>
      </div>
      <div class="personalList">
        <!--;input框&ndash;&gt;-->
        <div class="personalName">
          <span class="personalspanfour">基础分</span>
          <div style="text-align:left ">
            <Input  class="input" v-model="initScore" />
            <p>请输入初始积分</p>
          </div>
        </div>


        <div class="personalName">
          <span class="personalspanfour" style="margin-right:58px;">选项</span>
          <div style="text-align:left ">
            <RadioGroup v-model="scoreType" type="button" @on-change="handChangeType">
              <Radio label="选择加分"></Radio>
              <Radio label="选择减分"></Radio>
            </RadioGroup>
          </div>
        </div>

        <!--<button type="button" class="ivu-btn ivu-btn-ghost">&lt;!&ndash;&ndash;&gt; <i class="ivu-icon ivu-icon-refresh"></i> <span>刷新</span></button>-->
        <!--下拉框-->
        <div class="select">
          <h3>调整分</h3>
          <Row class="selects" style="display: inline-block">
            <Col v-show="addScoreDiv" span="12" class="selectone" style="padding-right:10px;width:200px">
              <Select v-model="addScore" style="width:200px" @on-change="changeScore">
                <Option v-for="item in addScoreList" :value="item.ItemID" :key="item.ItemID">{{ item.ItemDescription }}</Option>
              </Select>
              <span class="selespan"><span>{{addScoreLength}}</span>项工作工作表现正项加分</span>
            </Col>
            <Col v-show="divScoreDiv" span="12" class="selectone" style="padding-right:10px;width:200px">
              <Select v-model="model12" style="width:200px;" @on-change="changeScore">
                <Option v-for="item in divScoreList" :value="item.ItemID" :key="item.ItemID">{{ item.ItemDescription }}</Option>
              </Select>
              <span class="selespan"><span>{{divScoreLength}}</span>项工作工作表现负项减分</span>
            </Col>
          </Row>
        </div>

        <div class="personalName">
          <span class="personalspantwo">分值</span>
          <div style="text-align:left ">
            <Input  class="input" v-model="scoreValue" disabled />
            <p>正值为加分，负值为减分</p>
          </div>
        </div>

        <!--分管工作文本域-->
        <div class="textareaas">
          <span>摘要</span>
          <Input class="textareas" v-model="summary" type="textarea" :rows="4" placeholder="请输入..." />
        </div>

        <!-- <div class="personalName">
          <span class="personalspanone">其他分</span>
          <Input  class="input" v-model="" />
        </div>
        <div class="personalName">
          <span class="personalspanthree">分值</span>
          <Input  class="input"/>
        </div> -->
        <!--分管工作文本域-->
        <!-- <div class="textareaas">
          <span>摘要</span>
          <Input class="textareas"  type="textarea" :rows="4" placeholder="请输入..." />
        </div> -->
      </div>
    </div>

  </div>
</template>

<script>
import {queryScoreList} from '@/api/dicManage';
    export default {
        name: "intInfor",
      data () {
        return {
          cityList: [],
          model11: '',
          model12: [],
          // 分值
          scoreValue: null,
          // 摘要
          summary: null,
          // 基础分
          initScore: null,
          // 选择加分
          scoreType:'选择加分',
          // 加分div显示
          addScoreDiv:true,
          // 加分下拉框
          addScore:null,
          // 减分div显示
          divScoreDiv:false,
          // 减分下拉框
          divScore:null,
          // 添加分列表
          addScoreList: [],
          // 减分列表
          divScoreList:[],
          // 全部列表
          allScoreList:[],
          addScoreLength:null,
          divScoreLength:null,
          scoreID:null,
          ApplyItem: []
        }
      },
      created(){
        debugger;
        // 获取积分条目
        this.getScoreItem();
      },
      methods:{
        // 改变加分、减分类型
        handChangeType(val) {
          debugger;
          if(val == '选择加分') {// 点击的是选择加分类型
            this.addScoreDiv = true;
            this.divScoreDiv = false;
          } else if(val == '选择减分') {// 点击的是选择减分类型
            this.addScoreDiv = false;
            this.divScoreDiv = true;
          }
        },
        changeScore(val){
          debugger;
          var id = val;
          this.ApplyItem = [];
          for(let i = 0; i < this.allScoreList.length; i++){
            if(this.allScoreList[i].ItemID == id){
              this.scoreValue = this.allScoreList[i].ItemScore;
              // id
              this.scoreID = this.allScoreList[i].ItemID;
              var temp = {};
              temp.ItemID = this.scoreID;
              this.ApplyItem.push(temp);
            }
          }
        },
        getScoreItem(){
          debugger;
          queryScoreList().then(res => {
            debugger;
            if(res.IsSuccessful == true) {
              var row = res.Result.ScoreItemInfoList;
              this.allScoreList = row;
              var addList = [],divList = [];
              for(let i = 0; i < row.length; i++) {
                if(row[i].Type == 1) {// 加分
                  addList.push(row[i]);
                }else if(row[i].Type == 2){// 减分
                  divList.push(row[i]);
                }
              }
              this.addScoreList = addList;
              this.addScoreLength = addList.length;
              this.divScoreList = divList;
              this.divScoreLength = divList.length;
            }else{
              this.$Message.error(res.Reason);
            }
          })
        },
        // 获取数据
        showIntData() {
          debugger;
          var model12 = this.model12;
          var scoreValue = this.scoreValue;
          var summary = this.summary;
          var initScore = this.initScore;
          var ApplyItem = this.ApplyItem;
          var form = {
            model12,
            scoreValue,
            summary,
            initScore,
            ApplyItem
          }
          this.$emit('getInt',form);
        }
      }
    }
</script>

<style scoped>
  .personal{
    margin:15px auto 30px;
    text-align: center;
    display: -webkit-flex;
    -webkit-justify-content: center;
  }
  .centers{
    margin: 0 auto;
  }
  .personalList{
    text-align: center;
  }
  .personals{
    width:100%;
  }
  .personals span{
    width: 90px;
    font-size: 14px;
    color: darkgray;
    display: block;
    padding-left: 2px;
    border-left:3px solid #4990e2;
  }
  .select{
   display: -webkit-flex;
    justify-content: flex-start;
    align-items: center;
    margin:20px 0;
  }
  .select h3{
   color: black;
    margin-right: 40px;
    font-weight: 100;
    font-size: 16px;
  }
  .select .selects .selectone .selespan{
      font-size: 14px;
    margin-top: 10px;
    color: darkgray;
  }
  .selectone{
    margin-right: 38px;
    text-align: left;
  }
  /*input框样式*/
  .input{
    width:420px;
    line-height: 45px;
  }
  .personalName{
    display: -webkit-flex;
    -webkit-justify-content:flex-start;
    margin:12px 0;
  }
  .personalName p{
    font-size: 14px;
    color: darkgray;
  }
  .personalName .personalspanone{
    margin-right:45px;
    margin-top: 15px;
    font-size: 16px;
    color: black;
  }
  .personalName .personalspantwo{
    margin-right:59px;
    margin-top: 15px;
    font-size: 16px;
    color: black;
  }
  .personalName .personalspanthree{
    margin-right:62px;
    margin-top: 15px;
    font-size: 16px;
    color: black;
  }
  .personalName .personalspanfour{
    margin-right:42px;
    margin-top: 15px;
    font-size: 16px;
    color: black;
  }


  /*文本域*/
  .textareaas{
    display: -webkit-flex;
    justify-content: flex-start;
    align-items: center;
    margin: 12px 0;
  }
  .textareaas span{
    display: block;
    margin-right:60px;
    font-size: 16px;
    color: black;
  }
  .textareas{
    width: 420px;
  }
</style>
<style>
  .ivu-col ivu-col-span-12{
    width:200px;
  }
  /*.ivu-select-multiple .ivu-select-input{*/
    /*height: 44px;*/
  /*}*/
</style>
