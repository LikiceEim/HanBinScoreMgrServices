/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Setup.ClientSecret

 *文件名：  Main
 *创建人：  王颖辉
 *创建时间：2017-01-13
 *描述：客户端数据库连接和密钥
/************************************************************************************/
using System;
using System.Windows.Forms;
using iCMS.Common.Component.Tool;
namespace iCMS.Setup.ClientSecret
{
    #region 客户端数据库连接和密钥
    /// <summary>
    /// 客户端数据库连接和密钥
    /// </summary>
    public partial class Main : Form
    {
        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        public Main()
        {
            InitializeComponent();
        }
        #endregion

        #region 生成按钮
        /// <summary>
        /// 生成按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateBtn_Click(object sender, EventArgs e)
        {
            string original = originalTxt.Text.Trim();
            string key = EcanSecurity.GetClientKey(original);
            keyTxt.Text = key;
            string secret = EcanSecurity.GetClientSecret(key);
            secretTxt.Text = secret;
        }
        #endregion

        #region Tab选择
        /// <summary>
        /// Tab选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as TabControl).SelectedIndex == 0)
            {
                this.FindForm().Text = "客户端密钥生成工具";
            }
            else
            {
                this.FindForm().Text = "数据库连接字符串加、解密工具";
            }
        }
        #endregion

        #region 执行事件
        /// <summary>
        /// 执行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDo_Click(object sender, EventArgs e)
        {

            if (rtxtyuanshi.Text.Trim() == "" && rtxtjiami.Text.Trim() == "")
            {
                MessageBox.Show("请输入加密前或加密后字符串。");
                return;
            }

            if (rtxtyuanshi.Text.Trim() != "")
            {
                rtxtjiami.Text = EcanSecurity.Encode(rtxtyuanshi.Text.Trim());
                return;
            }
            else
            {
                rtxtyuanshi.Text = EcanSecurity.Decode(rtxtjiami.Text.Trim());
                return;
            }
        }
        #endregion
    
    }
    #endregion
}
