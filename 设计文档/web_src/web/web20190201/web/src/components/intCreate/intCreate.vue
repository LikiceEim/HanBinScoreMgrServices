<template>
  <div class="intCreate">
    <div class="intCre">
      <div class="craeteTop">
        积分申请
      </div>

      <div class="inputas">
        <div class="craeteBot">
          <div  class="select">
            <span style="margin-top:10px;display:inline-block;width:50px;">选项</span>
            <div>
              <RadioGroup v-model="scoreType" type="button" @on-change="handChangeType">
                <Radio label="选择加分"></Radio>
                <Radio label="选择减分"></Radio>
              </RadioGroup>
            </div>
          </div>
        </div>
      </div>

      <div class="craeteBot">
        <div class="select">
          <div class="iv-selectszong">
            <span style="display:inline-block;margin-top:10px;width:50px;">调整分</span>
            <div class="iv-selects" v-show="addScoreDiv">
              <Select v-model="addScore" style="width:200px" @on-change="changeScore">
                <Option v-for="item in addScoreList" :value="item.ItemID" :key="item.ItemID">{{ item.ItemDescription }}</Option>
              </Select>
              <span class="selespan"><span>{{addScoreLength}}</span>项工作表现正项加分</span>
            </div>
            <div class="iv-selects" style="margin-left:15px;" v-show="divScoreDiv">
              <Select v-model="divScore" style="width:200px;" @on-change="changeScore">
                <Option v-for="item in divScoreList" :value="item.ItemID" :key="item.ItemID">{{ item.ItemDescription }}</Option>
              </Select>
              <span class="selespan"><span>{{divScoreLength}}</span>项工作表现负项减分</span>
            </div>
          </div>
        </div>

        <!--分值input框-->
        <div class="select">
          <span style="display:inline-block;width:50px;text-align:left;">分值</span>
          <div class="iv-inputs" style="text-align:left ">
            <Input disabled v-model="scoreAdjust" class="inputs"/>
            <p>正值为加分，负值为减分</p>
          </div>
        </div>

        <!--文本域-->
        <div class="textareas">
          <span style="display:inline-block;width:50px;">摘要</span>
          <div class="iv-textareas">
            <Input class="textareass" v-model="value6" type="textarea" :rows="4" placeholder="请输入..." />
          </div>

        </div>

        <!--input框-->
        <!-- <div class="inputas">
          <span>其他分</span>
          <div class="iv-inputs">
            <Input size="large" class="inputs"/>
          </div>

        </div>
        <div class="inputas">
          <span>分值</span>
          <div class="iv-inputs">
            <Input size="large" class="inputs"/>
          </div>
        </div> -->
        <!--文本域-->
        <!-- <div class="textareas">
          <span>摘要</span>
          <div class="iv-textareas">
            <Input class="textareass" v-model="value6" type="textarea" :rows="4" placeholder="Enter something..." />
          </div>

        </div> -->

        <!-- add by lwj -上传文件 -->
        <div>
          <i-form :model="formData">
            <Upload
              ref="upload"
              :before-upload="handleUpload"
              :on-success="uploadSuccess"
              action="//192.168.0.103:2892/Hanbin/ScoreService/UploadFile">
              <Button icon="ios-cloud-upload-outline">浏览</Button>
            </Upload>
          </i-form>
        </div>

        <div v-if="isEmpty==false">
          <template v-for="(item, index) in file">
            <span :key="index" style="display:inline-block;width:calc(100% - 20px);overflow:hidden;">文件:{{item.file.name}}</span>
            <Icon v-show="item.isShowCheck" :key="index" style="color:#a6f2a6;font-weight:bold;" type="md-checkmark"></Icon>
            <Icon  @click="handleDelFile(item)" :key="index" style="color:red;font-weight:bold;cursor:pointer;" type="md-close"></Icon>
          </template>
        </div>
        <!-- add by lwj -上传文件 -->

        <!--点击按钮-->
        <div class="butt">
          <Button type="success" style="width:110px;" long @click="upload" :loading="loading">点击上传文件</Button>
          <h4>支持.doc .xls .jpg .png等格式文件</h4>
        </div>
        <div>
          <Button class="submit" style="width:110px;" long @click="submitForm" type="primary">提交</Button>
        </div>
      </div>
    </div>
  </div>

</template>

<script>
import {uploadFile, submitApply} from '@/api/intCreate'
import {deleteFile} from '@/api/leaderList'
import {queryScoreList} from '@/api/dicManage';
import Cookies from 'js-cookie';
    export default {
      name: "intCreate",
      data () {
        return {
          loading:false,
          // 判断是否文件上传了
          // isUploadFile:false,
          // 调整分
          scoreAdjust: 0,
          // 判断对象是否是空
          isEmpty:true,
          // 文件
          // file: {},
          file: [],
          UploadFileList:[],
          // 返回的文件地址
          fileAddress: '',
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
          value6:"",
          cityList: [],
          model11: '',
          model12: [],
          // 表单form
          formData:{
            FileSize: null,
            FileName: null,
            FileStream: null
          },
          // 干部ID
          OfficerID: null,
          scoreID:null,
          index:1
        }
      },
      created() {
        debugger;
        // 获取传过来的值
        var data = this.$route.query.OfficerID;
        this.OfficerID = data;
        var a = this.file;
        // 获取积分条目
        this.getScoreItem();
      },
      methods: {
        changeScore(val){
          debugger;
          var id = val;
          for(let i = 0; i < this.allScoreList.length; i++){
            if(this.allScoreList[i].ItemID == id){
              this.scoreAdjust = this.allScoreList[i].ItemScore;
              // id
              this.scoreID = this.allScoreList[i].ItemID;
            }
          }
          // this.scoreAdjust = val;
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
        // 提交整个表单
        submitForm() {
          debugger;
          // 判断是否有文件
          if(this.file.length != 0){
            // 先判断是不是最后一个文件没有提交
            if(this.file[this.file.length - 1].isShowCheck == false){
              this.$Message.info('请确保文件都以上传完毕，再提交！');
              return false;
            }
          }
          var OfficerID = this.OfficerID;
          var ScoreItemID = this.scoreID;
          var ApplySummary = this.value6;
          var ProposeID = Cookies.get('UserID');
          var UploadFileList = this.UploadFileList;
          var CurrentUserID = Cookies.get('UserID');
          var _data = {
            OfficerID,
            ScoreItemID,
            ApplySummary,
            ProposeID,
            UploadFileList,
            CurrentUserID
          }
          submitApply(_data).then(res => {
            debugger;
            if(res.IsSuccessful == true) {
              this.$Message.success('提交成功！');
              this.$router.push({name:'LeaderList'})
            }else{
              // this.$Message.error('提交失败！');
              this.$Message.error(res.Reason);
            }
          })
        },
        // 删除文件
        handleDelFile(data){
          debugger;
          var isUpload = data.isShowCheck;
          let id = data.index;
          var FileName = data.resFile;
          for(let i = 0;i < this.file.length;i++){
            if(id == this.file[i].index){
              if(isUpload == true){// 上传过的
                var _data = {
                  FileName:FileName
                }
                deleteFile(_data).then(res => {
                  debugger;
                  if(res.IsSuccessful == true){
                    this.file.splice(i,1);
                    this.UploadFileList.splice(i,1);
                    this.$Message.success('删除成功！');
                    return false;
                  }else{
                    this.$Message.error(res.Reason);
                  }
                })
              }else{// 未上传，直接删除
                this.file.splice(i,1);
                this.UploadFileList.splice(i,1);
                break;
              }
            }
          }
        },
        // 上传文件前的事件钩子
        handleUpload(file) {
          debugger;
          // 先判断是否是上个文件上传了
          if(this.file.length > 0){
            if(this.file[this.file.length-1].isShowCheck == false){
              this.$Message.info('请先上传文件：'+this.file[this.file.length-1].file.name);
              return false;
            }
          }
          if(file.size > 1024 * 1024 * 10){
            this.$Message.info('请上传小于10M的文件！');
            return false;
          }
          // 选择文件后 这里判断文件类型 保存文件 自定义一个keyid 值 方便后面删除操作
          let keyID = Math.random().toString().substr(2);
          file['keyID'] = keyID;
          // 保存文件到总展示文件数据里
          // this.file = file;
          // 保存文件到需要上传的文件数组里
          let index = this.index;
          this.file.push({
            file:file,
            isShowCheck:false,
            index:index,
            resFile:null
          })
          this.index = this.index + 1;
          // this.file.push(file);
          // 返回 falsa 停止自动上传 我们需要手动上传
          // 判断不为空
          // if(Object.keys(this.file).length == 0) {
          if(this.file.length == 0) {
            this.isEmpty = true;
          } else {
            this.isEmpty = false;
          }
          return false;
        },
        // 上传
        upload() {
          debugger;
          this.loading = true;
          // this.$refs.upload.post(this.file);
          if(this.file.length == 0){
            this.$Message.info('请先选择文件！');
            this.loading = false;
            return false;
          }
          var file = this.file[this.file.length-1].file;
          // 先判断是否是上传了上个文件
          // if(){
          //   if(this.file[this.file.length-1].isShowCheck == false){
          //     this.$Message.info('请先上传文件：'+file.name);
          //     return false;
          //   }
          // }
          this.formData.FileStream = file;
          var size = file.size;
          this.formData.FileSize = size;
          var name = file.name;
          this.formData.FileName = name;
          // 组织值，传值
          var formData = new FormData(this.formData);
          var that = this;
          formData.append('FileStream', this.formData.FileStream);
          formData.set('FileStream', this.formData.FileStream);
          // return false;
          // formData.append('FileSize', this.formData.FileSize);
          // formData.set('FileSize', this.formData.FileSize);
          // formData.append('FileName', this.formData.FileName);
          // formData.set('FileName', this.formData.FileName);
          uploadFile(formData, name).then(res => {
            debugger;
            this.loading = false;
            if(res.IsSuccessful == true){
              this.$Message.success('上传成功！');
              // this.isUploadFile = true;
              this.file[this.file.length-1].isShowCheck = true;
              this.file[this.file.length-1].resFile = res.Result.FilePath;
              this.UploadFileList.push(res.Result.FilePath);
            }else{
              this.$Message.error(res.Reason);
            }
          })

        },
        // 上传成功回调
        uploadSuccess() {
          debugger;

        },
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
        }
      }
    }
</script>

<style scoped>
  .intCreate{
    width:100%;
    /*margin:0 auto;*/

  }
  .intCre{
    width:50%;
    margin:0 auto;
    border:1px solid #fff;
    background:#f5f7f9 ;
    text-align: center;
    display: -webkit-flex;
    flex-direction: column;
    align-items: center;
  }
  .craeteTop{
    width:100%;
    height:65px;
    font-size: 24px;
    font-weight: 600;
    background: #cdd0d4;
    line-height: 65px;
    color: #323232;
  }
  .craeteBot{
    width:100%;
    background: #f5f7f9;
    margin:0 auto;
    display: -webkit-flex;
    flex-direction: column;
    text-align: center;
    padding-top: 30px;
    align-items: center;
  }
  .select{
    display: -webkit-flex;
    align-items: center;
    margin:20px 0;
    width: 60%;
  }
  .select h3{
    width: 15%;
    color: black;
    /*margin-right: 35px;*/
    text-align: left;
    font-weight: 100;
    font-size: 16px;

  }
  .select .iv-selectszong{
    display: -webkit-flex;
    width: 85%;
    text-align: left;
  }

  .iv-selects{
    display: -webkit-flex;
    text-align: left;flex-direction: column;
  }
  .iv-selects .selespan{
    font-size: 14px;
    margin-top: 10px;
    color: darkgray;
    display: block;
  }

  /*input框样式*/
  .inputas{
    width: 100%;
    margin-top: 15px;
    display: -webkit-flex;
  }
  .iv-inputs{
    width: 80%;
    text-align: left;
  }
  .inputas span{
    width: 15%;
    display: block;
    /*margin-right:50px;*/
    margin-top: 15px;
    text-align: left;
  }
  .inputas p{
    /*width: 15%;*/
    font-size: 14px;
    color:darkgray;
  }
  .inputs{
    width: 100%;
    line-height: 45px;
  }

  /*文本域*/
  .textareas{
    display: -webkit-flex;
    justify-content: flex-start;
    align-items: center;
    margin: 15px 0;
    width: 60%;
  }
  .textareas span{
    display: block;
    /*margin-right:60px;*/
    text-align: left;
    width: 15%;
  }
  .iv-textareas{
    width: 85%;
    text-align: left;
  }
  .textareass{
    width: 100%;
  }
  .butt{
    margin-top:10px;
  }
  .butt h4{
    margin-top:5px;
    color: #bfbfbf;
    font-size: 14px;
  }
  .submit{
    margin:30px 0;
  }

</style>
<style>
  /*.ivu-col ivu-col-span-12{*/

  /*}*/
  /*.ivu-select-multiple .ivu-select-input{*/
    /*height: 52px;*/
  /*}*/
  .ivu-radio-wrapper{
    font-size: 16px;
  }
  .ivu-tabs-nav-scroll{
    display: inline-block;
  }

  .ivu-btn-success{
    width: 45%;
    background-color: #bfbfbf;
    border-color: #bfbfbf;
  }
  /*.ivu-btn-primary{*/
    /*width: 150px;*/
  /*}*/

</style>
