<template>
  <div class="logManage">
    <div class="craeteTop">
      日志管理
    </div>
    <div >
      <div class="unitLIstFl">
        <!--input 搜索框-->
        <div class="logHeader">
          <span>身份</span>
          <Select class="ivSelect" v-model="roleID" style="width:200px">
            <Option v-for="item in cityList" :value="item.value" :key="item.value">{{ item.label }}</Option>
          </Select>

          <span>时间</span>
          <DatePicker type="date" v-model="startTime" placeholder="请选择开始日期" style="width: 150px;margin-left:15px;"></DatePicker>
          <span>-</span>
          <DatePicker class="ivSelect" type="date" v-model="endTime" placeholder="请选择结束日期" style="width: 150px;margin-left:0;"></DatePicker>

          <span>关键字</span>
          <Input  class="unltlistInput ivSelect" v-model="keyValue" placeholder="搜索用户ID，单位名称..." style="width:200px;" />
          <Button type="primary" @click="queryData">搜索</Button>
        </div>
      </div>


      <!--表格-->
      <div>
        <Table  border :columns="columns7" :data="data6" @on-sort-change='changeSort'></Table>
      </div>
      <!--分页器-->
      <div>
        <div class="unltlistfyq">
          <Page :total="total" @on-change="handleChangePage" :current="current" :page-size="pageSize" show-elevator />
        </div>
      </div>
    </div>
  </div>

</template>

<script>
  import {queryLog} from '@/api/logManage'
  import {queryRoleList} from '@/api/usersManage'
    export default {
      name: "logManage",
      data () {
        return {
          keyValue:"",
          cityList: [],
          // 身份
          roleID: null,
          // 时间
          startTime: null,
          endTime: null,
          // 总数
          total: 0,
          // 当前页
          current:1,
          // 显示条数
          pageSize:15,
          sort: '',
          order: 'asc',
          columns7: [
            {
              title: '身份',
              key: 'body',
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
              title: '账户',
              key: 'state'
            },
            {
              title: '所在单位',
              key: 'unit'
            },
            {
              title: '访问IP',
              key: 'IP'
            },
            {
              title: '方式',
              key: 'gets'
            },{
              title: '操作日志',
              key: 'users'
            },{
              title: '访问时间',
              key: 'time',
              sortable: true
            },
          ],
          data6: [],
        };
      },
      created() {
        // 获取身份
        this.getIdentity();
        this.queryData();
      },
      methods: {
        // 排序
        changeSort(key, type){
          debugger;
          var sort = key.key;
          if(key.key == 'time'){
            sort = 'AddDate';
          }
          this.sort = sort;
          this.order = key.order;
          this.queryData();
        },
        // 翻页
        handleChangePage(val){
          debugger;
          this.current = val;
          this.queryData();
        },
        // 获取身份
        getIdentity(){
          debugger;
          this.cityList = [];
          queryRoleList().then(res => {
            debugger;
            var Option = [];
            if(res.IsSuccessful == true) {
              Option.push({
                label: '全部',
                value: null
              })
              for(let i = 0; i < res.Result.RoleList.length; i++) {
                var tempObj = {};
                tempObj.value = res.Result.RoleList[i].RoleID;
                tempObj.label = res.Result.RoleList[i].RoleName;
                Option.push(tempObj);
              }
              this.cityList = Option;
            } else {
              this.$Message.error(res.Reason);
            }
          })
        },
        // 初始化查询数据
        queryData() {
          debugger;
          var RoleID = this.roleID;
          var BeginTime = this.startTime;
          var EndTime = this.endTime;
          var Keyword = this.keyValue;
          var Page = this.current;
          var PageSize = this.pageSize;
          var Sort = this.sort;
          var Order = this.order;
          var _data = {
            RoleID,
            BeginTime,
            EndTime,
            Keyword,
            Page,
            PageSize,
            Sort,
            Order
          }
          queryLog(_data).then(res => {
            debugger;
            if(res.IsSuccessful == true){
              this.total = res.Result.Total;
              var row = res.Result.LogList;
              var list = [];
              for(let i = 0; i < row.length; i++) {
                var tempObj = {};
                tempObj.body = row[i].RoleName;
                tempObj.state = row[i].UserToken;
                tempObj.unit = row[i].OrganName;
                tempObj.IP = row[i].IP;
                tempObj.gets = row[i].HTTPType;
                tempObj.users = row[i].Content;
                tempObj.time = row[i].OperationDate;
                list.push(tempObj);
              }
              this.data6 = list;
            }else{
              this.$Message.error(res.Reason);
            }
          })
        },
        show (index) {
          this.$Modal.info({
            title: 'User Info',
            content: `Name：${this.data6[index].name}<br>Age：${this.data6[index].age}<br>Address：${this.data6[index].address}`
          })
        },
        remove (index) {
          this.data6.splice(index, 1);
        }
      }
  }

</script>

<style scoped>
  .logManage{
    /*padding:20px 15px 0;*/
  }
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
    margin: 0 15px;
    height: 70px;
    line-height: 70px;
  }
  .logHeader span{
    font-size: 14px;
    color: #515a6e;
  }
  .ivSelect{
    margin:20px 50px 20px 15px;
  }
  .unltlistInput{
    margin:20px 15px 20px 15px;
  }
  .unltlistfyq{
    text-align: right;
    margin-right:15px;
    height:100px;
    line-height: 100px;
  }
</style>
