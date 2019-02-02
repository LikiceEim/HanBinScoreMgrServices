<template>
  <div>
    <div class="craeteTop">
      创建单位
    </div>
    <div class="createBt">
      <div class="createBtLf">
        <!--创建input-->
        <div class="inputas">
          <span>单位编码</span>
          <div class="inputasrig">
            <Input  class="inputs"   v-model="value1" @on-blur="btn1" placeholder="请输入..."/>
            <p>单位的编码为单位唯一识别ID, 不能重复</p>
          </div>
        </div>
        <div class="inputas">
          <span>单位全称</span>
          <div class="inputasrig">
            <Input class="inputs"  v-model="value2" @on-blur="btn2" placeholder="请输入..." />
            <p>单位的全称，最长不能超过20个字</p>
         </div>
        </div>
        <div class="inputas">
          <span>单位简称</span>
          <div class="inputasrig">
            <Input class="inputs" v-model="value3" @on-blur="btn3" placeholder="请输入..." />
            <p>单位的简称，最多不能超过10个字</p>
          </div>
        </div>
        <!--镇办 区政府-->
        <div class="createZb">
          <Tabs :animated="false" @on-click="tabBtn1" >
            <TabPane label="镇办" name="镇办" >
              <div>
                <RadioGroup  @on-change="butt1" v-model="phone">
                  <Radio label="重点发展区域镇办" ></Radio>
                  <Radio label="生态发展区域镇办"></Radio>

                </RadioGroup>
              </div>
            </TabPane>
            <TabPane label="区级部门" name="区级部门" >
              <div>
                <RadioGroup @on-change="butt2" v-model="phone1" >
                  <Radio label="党群部门"></Radio>
                  <Radio label="政府经济部门"></Radio>
                  <Radio label="政府非经济部门"></Radio>
                  <Radio label="驻区单位"></Radio>
                </RadioGroup>
              </div>
            </TabPane>
          </Tabs>
        </div>
        <!--创建按钮-->
        <div class="createAn">
          <Button type="primary" @click="cjbtn">创建</Button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
    export default {
        name: "createUnit",
      data () {
        return {
          value1: '',
          value2: '',
          value3: '',
          list:[],
          phone:'',
          phone1:''

        }

      },
      methods:{
        btn1(val){
          console.log(this.value1);
           this.value1=val.srcElement.value
        },
        btn2(val){
          console.log(this.value2)
          this.value2=val.srcElement.value
          // this.list.push(val.srcElement.value)
        },
        btn3(val){
         console.log(this.value3)
          this.value3=val.srcElement.value
          //this.list.push(val.srcElement.value)

        },
        tabBtn1(val){
          this.phone = '';
          this.phone1='';
          console.log(val)
        },
        butt1(val){
          this.phone=val;
        },

        butt2(val){
          this.phone1=val;
        },
        cjbtn(){
          this.list.push(this.value1,this.value2,this.value3,this.phone1,this.phone)
          console.log(this.list)
         this.$router.push("/UnitList")
          // this.$axios({
          //   url:"url",
          //   method:"post",
          //   data:{CjList:this.list}
          // }).then((res)=>{
          //   console.log(res)
          // })
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
.createBt{
  width: 100%;
  height:100%;

}
.createBtLf{
  width: 45%;
  text-align: center;
  margin-top:80px;
}
.createBtLf .inputas .inputs{
  width:50vh;
  line-height: 30px;
}
.inputas{
  margin-top: 30px;
  display: -webkit-flex;
  -webkit-justify-content: center;
}
.inputas span{
  margin-right:20px;
  display: block;
}
.inputas p{
 font-size: 14px;
  margin-top: 10px;
}
.inputasrig{
  text-align: left;
}
.createZb{
  margin-top: 30px;
}
.createAn{
    margin-top:40px;
  }

</style>
<style>
  .createZb .ivu-tabs-bar{
    border-bottom: 0px solid #dcdee2;
  }
  .ivu-tabs-nav{
    float:none;
    font-size: 16px;
  }

  .ivu-radio-wrapper{
    font-size: 14px;
  }
  .ivu-tabs-nav-scroll{
    display: inline-block;
  }
  .ivu-input-large{
    height:30px;
  }
</style>
