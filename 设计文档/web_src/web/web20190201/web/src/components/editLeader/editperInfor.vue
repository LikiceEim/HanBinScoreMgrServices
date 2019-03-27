<template>
    <div class="personal">
      <div class="centers">
        <div class="personals">
          <span> 个人信息</span>
        </div>
        <div class="personalList">
          <!--姓名-->
          <div class="personalName">
            <span class="personalspanone">姓名</span>
            <div style="text-align:left ">
              <Input  class="input" v-model="value1" />
              <p>姓名和身份证上保持一致</p>
            </div>
          </div>
          <!--性别-->
          <div class="sexs">
            <span>性别</span>
            <RadioGroup class="sex" v-model="button4" type="button" >
              <Radio label="男" class="man"></Radio>
              <Radio label="女" class="man"></Radio>
            </RadioGroup>
          </div>
          <!--身份证号-->
          <div class="personalName">
            <span class="personalspantwo">身份证号</span>
            <div style="text-align:left">
              <Input  class="input" v-model="value2"  @on-blur="btn2" />
              <p>干部身份的唯一标识</p>
            </div>
          </div>

          <!--选择出生年月-->
          <div class="personalName">
            <span class="personalspantwo">出生年月</span>
            <!-- <Row style="margin-top:15px;">
              <DatePicker type="date"  placeholder="请选择" v-model="birthday" style="width: 420px"></DatePicker>
            </Row> -->
            <div style="text-align:left ">
              <Input  class="input" v-model="birthday" disabled />
              <p>出生年月需要和身份证一致</p>
            </div>
          </div>
        </div>
      </div>

    </div>
</template>

<script>
    export default {
        name: "PerInfor",
        props:['Per'],
        
      data(){
        return{
          button4:"男",
          list:[],
          value1:"",
          value2:"",
          birthday:''
        }
      },
      watch: {
        Per: {
            handler(val, oldval) {
                this.setData();
            },
            deep: true
        }
      },
      methods:{
        // 获取值
        showPerData() {
          debugger;
          var name = this.value1;
          var sex = this.button4;
          var idCard = this.value2;
          var birthday = this.birthday;
          var form = {
            name, sex, idCard, birthday
          }
          this.$emit('getPer',form)
        },
        // 设置值
        setData(){
            debugger;
            var data = this.Per;
            this.value1 = data.Name;
            var sex = '';
            if(data.Gender == 1){
                this.button4 = '男';
            }else if(data.Gender == 2){
                this.button4 = '女';
            }
            // this.button4 = data.Gender;
            this.value2 = data.IdentifyNumber;
            this.birthday = data.Birthday;
        },
        btn2(val){
          debugger;
          console.log(val.srcElement.value)
          // 设置出生年月
          var birthday = val.srcElement.value;
          var birthdayYear = birthday.substr(6,8);
          // 格式化
          var left = birthdayYear.substring(0,4);
          var center = birthdayYear.substring(4,6);
          var right = birthdayYear.substring(6,8);
          var allBirthday = left + '-' + center + '-' + right;
          this.birthday = allBirthday;
        },
      }
    }
</script>

<style scoped>
  .personal{
    margin:30px auto;
    text-align: center;
    display: -webkit-flex;
    -webkit-justify-content: center;
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

  .personalList{
    text-align: center;
    margin: 20px auto;
    width: 100%;
  }
  .personalList span{
    color: black;
    font-size: 16px;
  }
  .personalList p{
    font-size: 14px;
    color:darkgray;
  }
  .personalName{
    display: -webkit-flex;
    -webkit-justify-content:flex-start;
  }
  .personalName .personalspanone{
   margin-right:50px;
    margin-top: 15px;
  }
  .personalName .personalspantwo{
    margin-right:20px;
    margin-top: 15px;
  }
  .sexs{
    height: 100px;
    line-height: 100px;
    text-align: left;
  }
  .sexs span{
    color: black;
    margin-right: 175px;
  }
  .input{
    width:420px;
    line-height: 60px;
  }
  .man{
    margin-right: 57px;
    width:55px;
    text-align: center;
  }
  .years{
    margin-top: 50px;
    width: 100%;
    display: -webkit-flex;

  }
 .years .yearsYear{
   margin-left: 20px;
   margin-right: 25px;
 }
  .years span{
    display: block;
  }
  .createAn{
    margin-top:20px;
  }
</style>
<style>

  .ivu-radio-group-button.ivu-radio-group-large .ivu-radio-wrapper{
    text-align: center;
  }


</style>
