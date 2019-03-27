<template>
    <div>
      <div class="craeteTop">
        字典管理
      </div>
      <div>
        <Tabs class="labels" :animated="false" @on-click="handClickIntegral" >
        <TabPane name="addTabPane" label="工作实绩正向加分规则" class="bd-botttom">
          <ul>
            <template v-for="(item, index) in addScoreList" >
              <li :key="index" class="lists">
                <h3 style="width:20%;">{{item.name}}</h3> 
                <span style="display:inline-block;width:40%;">{{item.ItemDescription}}</span>
                <span style="display:inline-block;width:10%;">
                  <!-- <span>+</span> -->
                  <span style="margin:0;">+{{item.ItemScore}}</span>
                </span>
                <h3 style="width:25%;">
                  <Button :data="item.jsonList" @click="bjbtn">编辑</Button>
                  <Button :data="item.jsonList"  @click="deleteBtn">删除</Button>
                </h3>
              </li>
            </template>
          </ul>
          <Modal  width="300"
                  :title="ForEditScore"
                  v-model="addScore"
                  class-name="vertical-center-modal">
            <div style="text-align:center">
              <div class="ps">
                <span>规范名称</span>
                <Input  class="input" v-model="ScoreEditName" placeholder="请输入..." style="width: 200px"/>
              </div>
              <div class="ps">
                <!-- <span>加分值</span> -->
                <span>{{editTitle}}</span>
                <Input  class="input" v-model="ScoreEditValue" placeholder="请输入..." style="width: 200px"/>
              </div>
            </div>
            <div slot="footer" style="text-align: center">
              <Button type="primary" @click="doEditScore">保存</Button>
            </div>
          </Modal>
        </TabPane>
        <TabPane name="divTabPane" label="工作实绩负向减分规则" class="bd-botttom">
          <ul>
            <template  v-for="(item, index) in divScoreList">
              <li :key="index" class="lists">
                <h3 style="width:20%;">{{item.name}}</h3> 
                <span style="display:inline-block;width:40%;">{{item.ItemDescription}}</span>
                <!-- <span>+</span> -->
                <span style="display:inline-block;width:10%;">{{item.ItemScore}}</span>
                <h3 style="width:25%;">
                  <Button :data="item.jsonList" @click="bjbtn">编辑</Button>
                  <Button :data="item.jsonList" @click="deleteBtn">删除</Button>
                </h3>
              </li>
            </template>
          </ul>
          <Modal  width="300"
                  title="编辑减分规格"
                  v-model="divScore"
                  class-name="vertical-center-modal">
            <div style="text-align:center">
              <div class="ps">
                <span>规范名称</span>
                <Input  class="input" placeholder="请输入..." style="width: 200px"/>
              </div>
              <div class="ps">
                <span>减分值</span>
                <Input  class="input" placeholder="请输入..." style="width: 200px"/>
              </div>
            </div>
            <div slot="footer" style="text-align: center">
              <Button type="primary" @click="doTjbtn">保存</Button>
            </div>
          </Modal>
        </TabPane>
      </Tabs>
          <!--新增图标-->
        <div class="IconBoxs">
          <div class="IconBox" @click="tjbtn">
            <Button v-show="addCicle" style="border: none;background: 0"  >
              <!--@click="addScore = true"-->
              <Icon class="Icons" type="ios-add-circle" />
              <span>新增加分规则</span>
            </Button>
            <Button v-show="divCicle" style="border: none;background: 0">
              <Icon class="Icons" type="ios-add-circle" />
              <span>新增减分规则</span>
            </Button>
              <Modal  width="300"
                      :title="modal11title"
                      v-model="modal11"
                      class-name="vertical-center-modal">
                <div style="text-align:center">
                  <div class="ps">
                    <span>规范名称</span>
                    <Input v-model="inpname" class="input" placeholder="请输入..." style="width: 200px"/>
                  </div>
                  <div class="ps">
                    <!-- <span>加分值</span> -->
                    <span>{{addTitle}}</span>
                    <Input v-model="inpval" class="input" placeholder="请输入..." style="width: 200px"/>
                  </div>
                </div>
                <div slot="footer" style="text-align: center">
                  <Button type="primary" @click="doTjbtn">添加</Button>
                </div>
              </Modal>

            <!--<Button  @click="xjbtn">编辑</Button>-->


          </div>
        </div>
      </div>

      <Modal v-model="isDeleteDialog" width="360">
        <p slot="header" style="color:#f60;text-align:center">
            <Icon type="information-circled"></Icon>
            <span>删除确认</span>
        </p>
        <div style="text-align:center">
            <p>是否删除？</p>
        </div>
        <div slot="footer">
            <i-button type="error" size="large" long :loading="modal_loading_del" @click="doDeleteBtn">删除</i-button>
        </div>
    </Modal>

    </div>
</template>

<script>
    import {queryScoreList, addScoreInfo, editScoreInfo, deleteScoreInfo} from '@/api/dicManage';
    import Cookies from 'js-cookie';
    export default {
        name: "dicManaged",
      data(){
          return{
            // 编辑框标题
            ForEditScore: null,
            // 加分悬浮框
            addScore: false,
            // 减分悬浮框
            divScore: false,
            // 加分列表
            addScoreList:[],
            // 减分列表
            divScoreList:[],
            modal11  : false,
            // 加分项
            addCicle:true,
            // 减分项
            divCicle:false,
            inpname:null,
            inpval:null,
            // 编辑操作悬浮框
            ScoreEditName: null,
            ScoreEditValue: null,
            ScoreEditItemID: null,
            ScoreEditType: null,
            // 删除操作ID
            ScoreDelID:null,
            // 删除弹出框
            isDeleteDialog: false,
            modal_loading_del: false,
            // 新增名称
            modal11title: '新增加分规则',
            addTitle:'加分值',
            editTitle:'加分值'
          }
      },
      created() {
        this.getScoreItem();
      },
      methods:{
        // 获取字典的积分列表
        getScoreItem() {
          debugger;
          queryScoreList().then(res => {
            debugger;
            if(res.IsSuccessful == true) {
              var row = res.Result.ScoreItemInfoList;
              var addList = [],divList = [],allList = [];
              var addIndex = 1,divIndex = 1;
              for(let i = 0; i < row.length; i++){
                var tempObj = {};
                if(row[i].Type == 1){// 加分
                  tempObj.ItemID = row[i].ItemID;// 主键ID
                  tempObj.ItemScore = row[i].ItemScore;// 积分值
                  tempObj.ItemDescription = row[i].ItemDescription;// 描述
                  tempObj.type = row[i].Type;// 类型
                  tempObj.name = '加分规则' + addIndex;
                  tempObj.jsonList = JSON.stringify(row[i]);
                  addIndex = addIndex + 1;
                  addList.push(tempObj);
                }else if(row[i].Type == 2){// 减分
                  tempObj.ItemID = row[i].ItemID;// 主键ID
                  tempObj.ItemScore = row[i].ItemScore;// 积分值
                  tempObj.ItemDescription = row[i].ItemDescription;// 描述
                  tempObj.type = row[i].Type;// 类型
                  tempObj.name = '减分规则' + divIndex;
                  tempObj.jsonList = JSON.stringify(row[i]);
                  divIndex = divIndex + 1;
                  divList.push(tempObj);
                }
                // tempObj.ItemID = row[i].ItemID;// 主键ID
                // tempObj.ItemScore = row[i].ItemScore;// 积分值
                // tempObj.ItemDescription = row[i].Type;// 类型
                // allList.push(tempObj);
              }
              this.addScoreList = addList;
              this.divScoreList = divList;
            }else{
              this.$Message.error(res.Reason);
            }
          })
        },
        // 点击面板
        handClickIntegral(name) {
          debugger;
          if(name == 'addTabPane'){
            this.addCicle = true;
            this.divCicle = false;
          } else if(name == 'divTabPane') {
            this.addCicle = false;
            this.divCicle = true;
          }
        },
        // 编辑
       bjbtn(evt){
          debugger;
          // 组织数据
          var row = evt.currentTarget.getAttribute('data');
          var rowData = JSON.parse(row);
          this.ScoreEditItemID = rowData.ItemID;
          this.ScoreEditValue = rowData.ItemScore;
          this.ScoreEditName = rowData.ItemDescription;
          this.ScoreEditType = rowData.Type;
          // var EditUserID = Cookies.get('UserID');
          if(rowData.Type == 1){// 加分
            this.ForEditScore = '编辑加分规则';
            this.editTitle = '加分值';
          }else if(rowData.Type == 2) {// 减分
            this.ForEditScore = '编辑减分规则';
            this.editTitle = '减分值';
          }
          this.addScore=true;
        },
        // 新增加分、减分项
        tjbtn(){
          debugger;
          if(this.addCicle == true){
            this.modal11title = '新增加分规则';
            this.addTitle = '加分值';
          }else if(this.divCicle == true){
            this.modal11title = '新增减分规则';
            this.addTitle = '减分值';
          }
          this.modal11=true;
        },
        // 执行添加积分项
        doTjbtn(){
          debugger;
          var name = this.inpname;
          var value = this.inpval;
          var type;
          if(this.addCicle == true) {// 新增增分项
            type = 1;
          } else if(this.divCicle == true) {// 新增减分项
            type = 2;
          }
          // 调用新增接口
          var _data = {
            ItemScore: value,
            ItemDescription: name,
            Type:type,
            AddUserID: Cookies.get('UserID')
          }
          addScoreInfo(_data).then(res => {
            debugger;
            if(res.IsSuccessful == true) {
              this.$Message.success('添加成功！');
              this.modal11=false;
              this.divCicle == false;
              // 请空值
              this.inpname = null;
              this.inpval = null;
              this.getScoreItem();
            }else{
              // this.$Message.error('添加失败！');
              this.$Message.error(res.Reason);
              // this.modal11=false;
              // this.getScoreItem();
            }
          })
        },
        // 执行编辑动作
        doEditScore(){
          debugger;
          var ItemID = this.ScoreEditItemID;
          var ItemScore = this.ScoreEditValue;
          var ItemDescription = this.ScoreEditName;
          var EditUserID = Cookies.get('UserID');
          var _data = {
            ItemID,
            ItemScore,
            ItemDescription,
            EditUserID
          }
          // 编辑接口
          editScoreInfo(_data).then(res => {
            debugger;
            if(res.IsSuccessful == true) {
              this.$Message.success('编辑成功！');
              this.addScore=false;
              this.getScoreItem();
            }else{
              // this.$Message.error('编辑失败！');
              this.$Message.error(res.Reason);
            }
          })
        },
        // 删除
        deleteBtn(evt){
          debugger;
          var row = evt.currentTarget.getAttribute('data');
          var rowData = JSON.parse(row);
          this.ScoreDelID = rowData.ItemID;
          this.isDeleteDialog = true;
        },
        // 执行删除操作
        doDeleteBtn(){
          debugger;
          this.modal_loading_del = true;
          var _data = {
            ItemID: this.ScoreDelID
          }
          deleteScoreInfo(_data).then(res => {
            debugger;
            this.modal_loading_del = false;
            if(res.IsSuccessful == true) {
              this.$Message.success('删除成功！');
              this.isDeleteDialog = false;
              this.getScoreItem();
            }else{
              // this.$Message.error('删除失败！');
              this.$Message.error(res.Reason);
            }
          })
        }
      }
    }
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
  }
  .labels{
    width: 700px;
    margin: 40px auto;
  }
  .bd-botttom li{
    height: 52px;
    border-bottom: 1px dashed slategray;
  }
  .lists h3{
    display: inline-block;
    font-size: 14px;
  }
  .lists span:nth-child(3){
    margin:0 0;
  }
  .lists span:nth-child(4){
    margin-left:0;
  }
  .lists span{
    margin:0 10px;
  }
  .IconBoxs{
    width: 700px;
    margin:0 auto;
  }
  .Icons{
    font-size: 30px;
    color: #2d8cf0;
  }
  .IconBox{
    width:280px;
    font-size: 14px;
    color: #2d8cf0;
  }
  .ps{
    width: 100%;
    height:60px;
    line-height:60px;
    text-align: center;
    display: -webkit-flex;
    -webkit-align-items: center;
  }
  .ps span{
    text-align: left;
    width: 24%;
    font-size:14px;
    color: black;
    display: block;
  }
  .ps .input{
    width: 70%;
    display: block;
  }
</style>
<style>

  .ivu-modal-header-inner{
    font-size: 18px;
  }
  /*删除下划线*/
  .ivu-tabs-bar{
   border-bottom: 0px solid #dcdee2;
    margin-bottom: 16px;
}
  .ivu-tabs-nav-container{
    font-size: 16px;
  }
</style>
