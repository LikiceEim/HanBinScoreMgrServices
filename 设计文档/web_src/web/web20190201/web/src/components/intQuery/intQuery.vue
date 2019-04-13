<template>
  <div>
    <div class="craeteTop">积分查询</div>
    <div class="unltlistBot">
      <!--input 搜索框-->
      <div class="logHeader">
        <span>单位分类</span>
        <Select v-model="UnitClassification" style="width:120px">
          <Option
            v-for="item in UnitClassificationOption"
            :value="item.value"
            :key="item.value"
          >{{ item.label }}</Option>
        </Select>

        <span>职位级别</span>
        <Select v-model="PositionValue" style="width:120px">
          <Option
            v-for="item in positionList"
            :value="item.value"
            :key="item.value"
          >{{ item.label }}</Option>
        </Select>

        <span>性别</span>
        <Select v-model="sexValue" style="width:120px">
          <Option v-for="item in sex" :value="item.value" :key="item.value">{{ item.label }}</Option>
        </Select>
        <span>年龄范围</span>
        <DatePicker
          class="yearsYear"
          v-model="startTime"
          type="year"
          placeholder="请选择开始时间"
          style="width:100px"
        ></DatePicker>-
        <DatePicker
          class="yearsYear"
          v-model="endTime"
          type="year"
          placeholder="请选择结束时间"
          style="width:100px"
        ></DatePicker>
        <span>关键字</span>
        <Input v-model="keyword" placeholder="搜索干部名称、身份证号..." style="width:157px;"/>
        <Button class="primary" type="primary" @click="getScoreList">搜索</Button>
      </div>
    </div>
    <div>
      <Table border :columns="columns7" :data="data6"></Table>
    </div>
    <!--分页器-->
    <div>
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
  </div>
</template>

<script>
import { queryUnitName } from "@/api/usersManage";
import { queryCarreLevel, queryLevelList } from "@/api/leaderList";
import { queryIntScoreList } from "@/api/intQuery";
import { queryUnitType,quertMainOrganType } from "@/api/unitList";
import Cookies from "js-cookie";
export default {
  name: "intQuery",
  props: ["int"],
  computed: {
    fathathint(val) {
      console.log(this.int);
      return this.int;
    }
  },
  watch: {
    fathathint(val) {
      // this.data6=val
    }
  },
  data() {
    return {
      // 分页总数
      total: null,
      // 当前页
      current: 1,
      // 显示条数
      pageSize: 15,
      sort: "",
      order: "asc",
      // 开始时间
      startTime: null,
      // 结束时间
      endTime: null,
      // 单位分类
      UnitClassification: null,
      UnitClassificationOption: [],
      // 职位分类
      positionList: [],
      PositionValue: -1,
      keyword: "",
      cityList: [],
      sex: [
        {
          value: null,
          label: "全部"
        },
        {
          value: "1",
          label: "男"
        },
        {
          value: "2",
          label: "女"
        }
      ],
      model1: "",
      sexValue: "",
      columns7: [
        {
          title: "积分",
          key: "int",
          columns: {
            width: "100px"
          },
          render: (h, params) => {
            debugger;
            if (params.index == 0) {
              return h("div", [
                h("img", {
                  attrs: {
                    src: "/static/image/first.png"
                  },
                  style: {
                    width: "40px",
                    height: "40px"
                  }
                }),
                h("span", this.formatterIntScore(params.row.int))
              ]);
            } else if (params.index == 1) {
              return h("div", [
                h("img", {
                  attrs: {
                    src: "/static/image/second.png"
                  },
                  style: {
                    width: "40px",
                    height: "40px"
                  }
                }),
                h("span", this.formatterIntScore(params.row.int))
              ]);
            } else if (params.index == 2) {
              return h("div", [
                h("img", {
                  attrs: {
                    src: "/static/image/third.png"
                  },
                  style: {
                    width: "40px",
                    height: "40px"
                  }
                }),
                h("span", this.formatterIntScore(params.row.int))
              ]);
            } else {
              return h(
                "span",
                // this.formatterIntScore(params.row.int)
                params.row.int
              );
            }
          }
        },
        {
          title: "姓名",
          key: "name"
        },
        {
          title: "性别",
          key: "sex",
          render: (h, params) => {
            return h("span", this.formatSex(params.row.sex));
          }
        },
        
        {
          title: "所在单位",
          key: "unit"
        },
        {
          title: "现任职务",
          key: "xianr"
        },
        {
          title: "级别",
          key: "jibie"
        },
        {
          title: "出生年月",
          key: "yer"
        },
        {
          title: "任职时间",
          key: "time"
        }
      ],
      data6: []
    };
  },
  created() {
    debugger;
    // 获取单位分类
    // queryUnitName().then(res => {
    //   debugger;
    //   if(res.IsSuccessful == true) {
    //     this.UnitClassificationOption = [];
    //     var Option = [];
    //     for(let i = 0; i < res.Result.OrganList.length; i++) {
    //       var tempObj = {};
    //       tempObj.label = res.Result.OrganList[i].OrganFullName;
    //       tempObj.value = res.Result.OrganList[i].OrganID;
    //       Option.push(tempObj);
    //     }
    //     this.UnitClassificationOption = Option;
    //     // this.UnitList = Option;
    //   } else {
    //     this.$Message.error(res.Reason);
    //   }
    // });
    quertMainOrganType().then(res => {
      debugger;
      this.UnitClassificationOption = [];
      if (res.IsSuccessful == true) {
        var row = res.Result.MainOrganTypeItemList;
        var selectData = [];
        selectData.push({
          label: "全部",
          value: null
        });
        for (let i = 0; i < row.length; i++) {       
            var tempObj = {};
            tempObj.label = row[i].OrganTypeName;
            tempObj.value = row[i].OrganTypeID;
            selectData.push(tempObj);         
        }
        this.UnitClassificationOption = selectData;
      } else {
        this.$Message.error(res.Reason);
      }
    });

    // queryUnitType().then(res => {
    //   debugger;
    //   this.UnitClassificationOption = [];
    //   if (res.IsSuccessful == true) {
    //     var row = res.Result.CategoryList;
    //     var selectData = [];
    //     selectData.push({
    //       label: "全部",
    //       value: null
    //     });
    //     for (let i = 0; i < row.length; i++) {
    //       for (let j = 0; j < row[i].OrganTypeList.length; j++) {
    //         var tempObj = {};
    //         tempObj.label = row[i].OrganTypeList[j].OrganTypeName;
    //         tempObj.value = row[i].OrganTypeList[j].OrganTypeID;
    //         selectData.push(tempObj);
    //       }
    //     }
    //     this.UnitClassificationOption = selectData;
    //   } else {
    //     this.$Message.error(res.Reason);
    //   }
    // });
    // 获取职位
    // queryCarreLevel().then(res => {
    //   debugger;
    //   if(res.IsSuccessful == true) {
    //     var row = res.Result.PositionList;
    //     var list = [];
    //     list.push({
    //       label: '全部',
    //       value: null
    //     })
    //     for(let i = 0; i < row.length; i++) {
    //       var tempObj = {};
    //       tempObj.label = row[i].PositionName;
    //       tempObj.value = row[i].PositionID;
    //       list.push(tempObj);
    //     }
    //     this.positionList = list;
    //   }else{
    //     this.$Message.error(res.Reason);
    //   }
    // });
    queryLevelList().then(res => {
      debugger;
      if (res.IsSuccessful == true) {
        var row = res.Result.LevelList;
        var list = [];
        list.push({
          label: "全部",
          value: null
        });
        for (let i = 0; i < row.length; i++) {
          var tempObj = {};
          tempObj.label = row[i].LevelName;
          tempObj.value = row[i].LevelID;
          list.push(tempObj);
        }
        this.positionList = list;
      } else {
        this.$Message.error(res.Reason);
      }
    });
    // 查询积分列表
    this.getScoreList();
  },
  methods: {
    // 格式化
    formatSex(data) {
      if (data == 1) {
        return "男";
      } else if (data == 2) {
        return "女";
      }
    },
    // 翻页
    handleChangePage(val) {
      debugger;
      this.current = val;
      this.getScoreList();
    },
    // 格式化积分排名
    formatterIntScore(data) {
      return data;
    },
    // 查询积分列表
    getScoreList() {
      debugger;
      // 组织数据
      var OrganTypeID = this.UnitClassification; // 单位类型ID
      var LevelID = this.PositionValue; // 级别ID
      var BirthdayFrom = this.startTime; // 开始年龄
      var BirthdayTo = this.endTime; // 结束年龄
      var Gender = this.sexValue; // 性别
      var Keyword = this.keyword; // 关键词
      var Page = this.current; // 页码
      var PageSize = this.pageSize; // 页数
      var CurrentUserID = Cookies.get("UserID");
      var _data = {
        OrganTypeID,
        LevelID,
        BirthdayFrom,
        BirthdayTo,
        Gender,
        Keyword,
        Page,
        PageSize,
        CurrentUserID
      };
      this.data6 = [];
      queryIntScoreList(_data).then(res => {
        debugger;
        if (res.IsSuccessful == true) {
          this.total = res.Result.Total;
          var row = res.Result.QueryScoreItemList;
          var list = [];
          for (let i = 0; i < row.length; i++) {
            var tempObj = {};
            tempObj.int = row[i].CurrentScore + "分"; // 积分
            tempObj.CurrentScore = row[i].CurrentScore; // 分数
            tempObj.name = row[i].Name; // 名称
            tempObj.sex = row[i].Gender; // 性别
            tempObj.yer = row[i].Birthday.split(" ")[0]; // 出生年月
            tempObj.unit = row[i].OrganFullName; // 所在单位
            tempObj.xianr = row[i].PositionName; // 现任职务
            tempObj.jibie = row[i].LevelName; // 级别
            tempObj.time = row[i].OnOfficeDate.split(" ")[0]; // 任职时间
            tempObj.LevelID = row[i].LevelID;
            tempObj.OfficerID = row[i].OfficerID;
            tempObj.OrganID = row[i].OrganID;
            tempObj.OrganTypeID = row[i].OrganTypeID;
            tempObj.PositionID = row[i].PositionID;
            list.push(tempObj);
          }
          this.data6 = list;
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
.logHeader {
  margin: 0 15px;
  height: 70px;
  line-height: 70px;
}
.logHeader span {
  font-size: 14px;
  color: #515a6e;
}

.unltlistfyq {
  text-align: right;
  margin-right: 15px;
  height: 100px;
  line-height: 100px;
}
.unltlistInput {
  margin-left: 10px;
}
.ivSelect {
  margin: 0 45px 0 10px;
}
.primary {
  margin-left: 30px;
}
</style>

<style>
.ivu-table-wrapper {
  margin: 0 15px;
}
</style>
