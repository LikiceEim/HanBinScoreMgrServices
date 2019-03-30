<template>
    <div>
      <div class="craeteTop">
        干部列表
      </div><span>切换图形显示</span>
      <div class="unltlistBot">
        <!--所属分类-->
        <div class="unitLIstFl">
          <div>
            <span class="unitLIstspan">单位分类 </span>
            <Select v-model="UnitValue" style="width:180px"  >
              <Option  v-for="item in cityList" :value="item.value" :key="item.value">{{ item.label }}</Option>
            </Select>
            <span class="unitLIstspans">级别 </span>
            <Select v-model="PositionValue" style="width:180px" >
              <!--@on-change="selectBtn"-->
              <Option v-for="item in positionList" :value="item.value" :key="item.value">{{ item.label }}</Option>
            </Select>
            <!--属搜input go-->
            <span class="unitLIstspans">关键字 </span>
            <Input class="unltlistInput" style="width:240px;" v-model="Keyword" placeholder="搜索干部名称，身份证号或者单位名称..."/>
            <Button type="primary" @click="getLeaderList">搜索</Button>
          </div>
         <div>
           <Button to="CreateLeader" type="primary">创建干部</Button>
         </div>

        </div>
        <!--干部明细列表-->
        <div>
          <Table  border :columns="columns7" :data="data6" @on-sort-change='changeSort'></Table>
        </div>

      
        <!--分页器-->
          <div class="unltlistfyq">
            <Page :total="total" :current="current" :page-size="pageSize" @on-change="handleChangePage" show-total />
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
            <i-button type="error" size="large" long :loading="modal_loading" @click="doDelLeader">删除</i-button>
        </div>
    </Modal>

    <Modal v-model="isRetireDialog" width="360">
        <p slot="header" style="color:#f60;text-align:center">
            <Icon type="information-circled"></Icon>
            <span>退休确认</span>
        </p>
        <div style="text-align:center">
            <p>是否继续退休？</p>
        </div>
        <div slot="footer">
            <i-button type="info" size="large" long :loading="modal_loading_retire" @click="doRetireLeader">确认</i-button>
        </div>
    </Modal>

    </div>
</template>

<script>
  import {queryUnitType, queryCarreLevel, quertLeaderList, deleteLeaderInfo, queryLevelList, retireLeaderInfo} from '@/api/leaderList'
import { timingSafeEqual } from 'crypto';
import Cookies from 'js-cookie';
    export default {
      name: "leaderList",
      props:['td'],
      computed:{
          fathathtd(val){
            // console.log(this.td)
            return this.td
          }
      },
      watch:{
        fathathtd(val){
          this.data6=val
        }
      },
      data () {
        return {
          // 总数
          total: 0,
          // 当前页
          current:1,
          // 显示条数
          pageSize:15,
          sort: '',
          order: 'asc',
          // 单位分类
          cityList: [],
          // 职位分类
          positionList:[],
          Keyword:"",
          data6:[],
          list:[],
          UnitValue: -1,
          PositionValue: -1,
          columns7: [
            {
              title: '姓名',
              key: 'name',
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
              title: '性别',
              key: 'sex',
              render: (h,params)=>{
                  return h('span',
                    this.formatSex(params.row.sex)
                  )
                },
              sortable: true
            },
            {
              title: '出生年月',
              key: 'birth',
              sortable: true
            },
            {
              title: '所在单位',
              key: 'unit',
              sortable: true
            },
            {
              title: '现任职务',
              key: 'present',
              sortable: true
            },
            {
              title: '级别',
              key: 'rank',
              sortable: true
            },
            {
              title: '任职时间',
              key: 'time',
              sortable: true
            },
            {
              title: '积分',
              key: 'integral',
              sortable: true
            },
            {
              title: '操作',
              key: 'action',
              width: 150,
              align: 'center',
              render: (h, params) => {
                return h('div', [
                  
                  h('Tooltip',{
                    props:{
                      placement: 'top',
                      transfer: true,
                      content: ' 编辑'
                    }
                  },[                  
                  h('Icon', {
                    props: {
                      type: 'md-create',
                    },
                    style: {
                      marginRight: '5px',
                      color:"#2d8cf0",
                      fontSize:"18px",
                      cursor:"pointer",
                    },
                    on: {
                      click: () => {
                        // this.show(params.leaPerInfor)
                        debugger;
                        var row = params;
                        var data = params.row;
                        // this.$router.push("LeaPerInfor")
                        this.$router.push({path:"LeaPerInfor", query:data})
                      }
                    }
                  },)]),
                  h('Tooltip',{
                      props:{
                        placement: 'top',
                        transfer: true,
                        content: '退休'
                      }
                    },[h('Icon',{
                      props:{
                        type:'md-person',// 删除
                      },
                      style: {
                        marginRight: '5px',
                        color:"#2d8cf0",
                        fontSize:"18px",
                        cursor:"pointer"
                      },
                      on: {
                        click: () => {
                          this.retireLeader(params);
                        }
                      }
                    })]),
                  h('Tooltip',{
                      props:{
                        placement: 'top',
                        transfer: true,
                        content: '删除'
                      }
                    },[h('Icon',{
                      props:{
                        type:'md-trash',// 删除
                      },
                      style: {
                        marginRight: '5px',
                        color:"red",
                        fontSize:"18px",
                        cursor:"pointer"
                      },
                      on: {
                        click: () => {
                          this.delLeader(params);
                        }
                      }
                    })])
                ]);
              }
            }
          ],
          // 为了删除
          isDeleteDialog: false,
          isDeleteID:null,
          modal_loading:false,
          // 为了退休
          isRetireDialog:false,
          isRetireID:null,
          modal_loading_retire:false
        }
      },
      created() {
        debugger;
        // 获取单位分类
        this.cityList = [];
        queryUnitType().then(res => {
          debugger;
          if(res.IsSuccessful == true) {
            var row = res.Result.OrganList;
            var selectData = [];
            selectData.push({
              label: '全部',
              value: null
            })
            for(let i = 0; i < row.length; i++) {
              var tempObj = {};
              tempObj.label = row[i].OrganFullName;
              tempObj.value = row[i].OrganID;
              selectData.push(tempObj);
            }
            this.cityList = selectData;
          } else {
            this.$Message.error(res.Reason);
          }
        });
        // 获取职位
        queryLevelList().then(res => {
          debugger;
          if(res.IsSuccessful == true) {
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
            this.positionList = list;
          }else{
            this.$Message.error(res.Reason);
          }
        });
        // 获取干部列表
        this.getLeaderList();
      },
      methods:{
        // 退休
        retireLeader(val){
          debugger;
          var OfficerID = val.row.OfficerID;
          this.isRetireID = OfficerID;
          this.isRetireDialog = true;
          this.modal_loading_retire = false;
        },
        doRetireLeader(){
          debugger;
          this.modal_loading_retire = true;
          var CurrentUserID = Cookies.get('UserID');
          var _data = {
            OfficerID: this.isRetireID,
            CurrentUserID:CurrentUserID
          }
          retireLeaderInfo(_data).then(res => {
            debugger;
            this.modal_loading_retire = false;
            if(res.IsSuccessful == true){
              this.$Message.success('操作成功！');
              this.isRetireDialog = false;
              // 查询
              this.getLeaderList();
            }else{
              this.$Message.error(res.Reason);
            }
          })
        },
        // 格式化
        formatSex(data){
          if(data == 1){
            return '男';
          }else if(data == 2){
            return '女';
          }
        },
        // 排序
        changeSort(key, type){
          debugger;
          var sort = key.key;
          if(key.key == 'name'){
            sort = 'Name';
          }else if(key.key == 'sex'){
            sort = 'Gender';
          }else if(key.key == 'birth'){
            sort = 'Birthday';
          }else if(key.key == 'unit'){
            sort = 'OrganFullName';
          }else if(key.key == 'present'){
            sort = 'PositionName';
          }else if(key.key == 'rank'){
            sort = 'LevelName';
          }
          else if(key.key == 'time'){
            sort = 'OnOfficeDate';
          }else if(key.key == 'integral'){
            sort = 'CurrentScore';
          }
          this.sort = sort;
          this.order = key.order;
          this.getLeaderList();
        },
        // 翻页
        handleChangePage(val){
          debugger;
          this.current = val;
          this.getLeaderList();
        },
        // 删除
        delLeader(val){
          debugger;
          var OfficerID = val.row.OfficerID;
          this.isDeleteID = OfficerID;
          this.isDeleteDialog = true;
          this.modal_loading = false;
        },
        // 执行删除
        doDelLeader(){
          debugger;
          this.modal_loading = true;
          var _data = {
            OfficerID: this.isDeleteID
          }
          deleteLeaderInfo(_data).then(res => {
            debugger;
            this.modal_loading = false;
            if(res.IsSuccessful == true){
              this.$Message.success('删除成功！');
              this.isDeleteDialog = false;
              // 查询
              this.getLeaderList();
            }else{
              // this.$Message.error('删除失败！');
              this.$Message.error(res.Reason);
            }
          })
        },
        // 获取干部列表
        getLeaderList() {
          debugger;
          var OrganizationID = this.UnitValue;// 单位类型ID
          var LevelID = this.PositionValue;// 级别
          var Keyword = this.Keyword;// 关键词
          var Page = this.current;// 页码
          var PageSize = this.pageSize;// 页数
          var Sort = this.sort;
          var Order = this.order;
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
          quertLeaderList(_data).then(res => {
            debugger;
            if(res.IsSuccessful == true) {
              var row = res.Result.OfficerInfoList;
              this.total = res.Result.Total;
              var list = [];
              for(let i = 0; i < row.length; i++) {
                var tempObj = {};
                tempObj.name = row[i].Name;// 姓名
                tempObj.sex = row[i].Gender;// 性别：1男2女

                var time1 = row[i].Birthday;
                time1 = time1.split(' ')[0];

                console.log(time1);

                var yyMM= time1.substr(0,7);
                tempObj.birth = time1;// 出生年月

                // tempObj.birth = row[i].Birthday;// 出生年月
                tempObj.unit = row[i].OrganizationName;// 所在单位
                tempObj.present = row[i].PositionName;// 现任职务
                tempObj.rank = row[i].LevelName;// 级别

                var time = row[i].OnOfficeDate;
                time = time.split(' ')[0];

                var yyMM = time.substr(0, 7);
                tempObj.time = yyMM;// 任职时间
                // tempObj.time = row[i].OnOfficeDate;// 任职时间
                tempObj.integral = row[i].CurrentScore;// 积分
                tempObj.IdentifyNumber = row[i].IdentifyNumber;// 身份证号
                tempObj.LevelID = row[i].LevelID;// 级别ID
                tempObj.OfficerID = row[i].OfficerID;// 干部ID
                tempObj.OrganizationID = row[i].OrganizationID;// 所在单位ID
                tempObj.PositionID = row[i].PositionID;// 职务ID
                list.push(tempObj);
              }
              this.data6 = list;
            }else {

            }
          })
        },
        btn1(val){
          //debugger;
          console.log(val),
          this.list.push(val)
          // this.$emit("selectBtn",this.list)

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

  .unitLIstFl{
    padding:0 15px;
    height: 70px;
    line-height: 70px;
    display:-webkit-flex;
    -webkit-justify-content: space-between;
  }
  .unitLIstspan{
    margin-right:10px;
    font-size: 14px;
    color: #515a6e;
  }
  .unitLIstspans{
    margin:0 10px 0 60px;
    font-size: 14px;
    color: #515a6e;
  }
  .unltlistInput{
    width:180px;
    margin:0px 10px 0 0px;
  }
  .unltlistfyq{
    margin-right:15px;
    line-height: 100px;
    text-align: right;
  }

</style>
<style>


</style>
