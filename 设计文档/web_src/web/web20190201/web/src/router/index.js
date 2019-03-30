import Vue from 'vue'
import Router from 'vue-router'
import HomePages from '@/pages/homePages'
import CreateUnit from '@/pages/createUnit'
import UnitList from '@/pages/unitList'
import Login from '@/pages/login'
import Mains from '@/pages/mains'
import LeaderList from '@/pages/leaderList'
import LeaPerInfor from '@/pages/leaPerInfor'
import CreateLeader from '@/pages/createLeader'
import DicManaged from '@/pages/dicManaged'
import UsersManage from '@/pages/usersManage'
import LogManage from '@/pages/logManage'
import Backups from '@/pages/backups'
import IntQuery from '@/pages/intQuery'
import IntCreate from '@/pages/intCreate'
import ChinaCompany from '@/pages/chinaCompany'
// 引入分维度展示界面
import dimensionalPre from '@/pages/dimensionalPre'
// 引入审批展示界面
import approval from '@/pages/approval'

import preIndex from '@/pages/preindex'

import EditLeader from '@/pages/editLeader'
import LeaderGraphic from '@/pages/leaderGraphic'

Vue.use(Router)

// add by lwj---根据角色分配权限菜单---2018.12.16
import Cookies from 'js-cookie'

// add by lwj---根据角色分配权限菜单---2018.12.16 end
var roleID = Cookies.get('RoleID');
debugger;
export default new Router({
  mode:'history',
  routes: [
    {
      path: '/',
      name: 'Login',
      component: Login,
      redirect: '/Login',
      children:[
        {
          path: 'Login',
          name: 'Login',
          component: Login
        }
      ]
    },
    {
      path: '/LeaPerInfor',
      name: 'LeaPerInfor',
      component: LeaPerInfor,
      redirect: '/Mains/LeaPerInfor',
    
    },
    {
      path: '/',
      name: 'preindex',
      component: preIndex,
      redirect: '/preIndex',
      children:[
        {
          path: 'preIndex',
          name: 'preIndex',
          component: preIndex
        }
      ]
    },
    {
      path: '/',
      name: 'home',
      component: Mains,
      redirect: '/HomePages',
      children:[
        {
          path: 'HomePages',
          name: 'HomePages',
          component: HomePages
        }
      ]
    },
    {
      path: '/Mains',
      name: 'Mains',
      component: Mains,
      children:[
        {
          path: 'HomePages',
          name: 'HomePages',
          component: HomePages
        },{
          path:'CreateUnit',
          name:'CreateUnit',
          component:CreateUnit,
          // hidden: (roleID!=1&&roleID!=3)?true:false
        },{
          path:'UnitList',
          name:'UnitList',
          component:UnitList,
          // hidden: (roleID!=1&&roleID!=3)?true:false
        },{
          path:'LeaderList',
          name:'LeaderList',
          component:LeaderList,
          // hidden: (roleID!=1&&roleID!=4)?true:false
        },
        {
          path:'LeaderGraphic',
          name:'LeaderGraphic',
          component:LeaderGraphic,
          // hidden: (roleID!=1&&roleID!=4)?true:false
        },{
          path:'LeaPerInfor',
          name:'LeaPerInfor',
          component:LeaPerInfor,
        },{
          path:'CreateLeader',
          name:'CreateLeader',
          component:CreateLeader,
        },{
          path:'DicManaged',
          name:'DicManaged',
          component:DicManaged,
        },{
          path:'UsersManage',
          name:'UsersManage',
          component:UsersManage,
        },{
          path:'LogManage',
          name:'LogManage',
          component:LogManage,
        },{
          path:'Backups',
          name:'Backups',
          component:Backups,
        },{
          path:'IntQuery',
          name:'IntQuery',
          component:IntQuery,
        },
        {
          path:'dimensionalPre',
          name:'dimensionalPre',
          component:dimensionalPre
        },
        {
          path:'IntCreate',
          name:'IntCreate',
          component:IntCreate,
        },{
          path:'ChinaCompany',
          name:'ChinaCompany',
          component:ChinaCompany,
        },{
          path:'approval',
          name:'approval',
          component:approval
        },{
          path:'editLeader',
          name:'editLeader',
          component:EditLeader
        }

      ]
     },
  ]
})

// const LOGIN_PAGE_NAME = "login";
// router.beforeEach((to,from,next)=>{
//   iView.LoadingBar.start();
//   const token = storage.getStorage('token');
//   const access = storage.getStorage('access');
//   if(!token && to.name !== LOGIN_PAGE_NAME){
//     // 未登录且要跳转的页面不是登录页
//     next({
//       name: LOGIN_PAGE_NAME // 跳转到登录页
//     })
//   } else if(!token && to.name === LOGIN_PAGE_NAME){
//     // 未登陆且要跳转的页面是登录页
//     next() // 跳转
//   }else if(token && to.name ===LOGIN_PAGE_NAME ){
//     // 已登录且要跳转的页面是登录页
//     next({name:'project'}) // 跳转到home页
//   }else{
//     if(to.meta == undefined || to.meta.access == undefined)
//       next();
//     else if(to.meta != undefined && to.meta.access != undefined){
//       if(to.meta.access.length>0) {
//         if(to.meta.access.indexOf(access)>-1)next();
//         else next({name:'error_401'})
//       }
//       else next();
//     }else
//       next();
//   }
// })
// router.afterEach(to => {
//   iView.LoadingBar.finish()
//   window.scrollTo(0, 0)
// })

//export default router
