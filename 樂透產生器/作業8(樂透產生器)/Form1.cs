using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 作業8_樂透產生器_
{
    public partial class Form1 : Form
    {

        List<int> myWinWeiList = new List<int>();//儲存威力彩開獎號碼的list
        List<int> myWeiList = new List<int>();//儲存威力彩準備對獎號碼list
        List<int> myWinJingList = new List<int>();//儲存今彩539開獎號碼的list
        List<int> myJingList = new List<int>();//儲存今彩539準備對獎號碼list
        List<int> myWinFourList = new List<int>();//儲存四星彩開獎號碼的list
        List<int> myFourList = new List<int>();//儲存四星彩準備對獎號碼list


        //***********************************威力彩*******************************************
        public Form1()
        {
            InitializeComponent();
            //未開獎前對獎按鈕無法作用
            btnWeiMatch.Enabled = false;
            btnJingMatch.Enabled = false;
            btnFourMatch.Enabled = false;
        }

        //威力彩開獎
        private void btnWeiWinNumber_Click(object sender, EventArgs e)
        {
            //若已經有開獎，則先清除開獎號碼
            if (myWinWeiList.Count>0)
            {
                myWinWeiList.Clear();
            }

            //產生號碼池
            List<int> myNumPool = new List<int>();
            for (int i= 0;i<38;i+=1)
            {
                myNumPool.Add(i + 1);
            }

            Random myRand = new Random();


            //特別號先開獎
            int indexWinSpecial = myRand.Next(8);//產生特別號碼隨機數
            myWinWeiList.Add(myNumPool[indexWinSpecial]);//寫進開獎號碼myWinWeiList

            //主號碼開獎
            for (int i=0;i<6;i+=1) 
            {               
                int indexWinMain = myRand.Next(myNumPool.Count);//產生主號碼隨機數
                myWinWeiList.Add(myNumPool[indexWinMain]);//寫進開獎號碼myWinWeiList
                myNumPool.RemoveAt(indexWinMain);//號碼池刪除已開獎號碼，避免開獎號碼重複
            }


            myWinWeiList.Sort(1, 6, null);//將主號碼進行排序

            //從主號碼imagelist將開獎號碼圖片抓出來輸出
            pbWei1.Image = imagelistNum.Images[myWinWeiList[1]-1];
            pbWei2.Image = imagelistNum.Images[myWinWeiList[2]-1];
            pbWei3.Image = imagelistNum.Images[myWinWeiList[3]-1];
            pbWei4.Image = imagelistNum.Images[myWinWeiList[4]-1];
            pbWei5.Image = imagelistNum.Images[myWinWeiList[5]-1];
            pbWei6.Image = imagelistNum.Images[myWinWeiList[6]-1];
            //從特別號碼imagelist將開獎號碼圖片抓出來輸出
            pbWeiSpecial.Image = imagelistNumSpecial.Images[myWinWeiList[0]-1];

            //啟用對獎按鈕
            btnWeiMatch.Enabled = true;     
        }

        //威力彩對獎
        private void btnWeiMatch_Click(object sender, EventArgs e)
        {
            if ((tbWei1.Text.Length > 0)&& (tbWei2.Text.Length > 0) && (tbWei3.Text.Length > 0)&&
                (tbWei4.Text.Length > 0) && (tbWei5.Text.Length > 0) && (tbWei6.Text.Length > 0) && 
                (tbWeiSpecial.Text.Length > 0))//驗證是否有輸入號碼
            {
                //驗證輸入的號碼格式是否正確(非其他文字、小數...)
                bool ifWei1 = false;
                bool ifWei2 = false;
                bool ifWei3 = false;
                bool ifWei4 = false;
                bool ifWei5 = false;
                bool ifWei6 = false;
                bool ifWeiSpecial = false;

                
                int Wei1 = 0, Wei2 = 0, Wei3 = 0, Wei4 = 0, Wei5 = 0, Wei6 = 0, WeiSpecial = 0;


                ifWei1 = System.Int32.TryParse(tbWei1.Text, out Wei1);
                ifWei2 = System.Int32.TryParse(tbWei2.Text, out Wei2);
                ifWei3 = System.Int32.TryParse(tbWei3.Text, out Wei3);
                ifWei4 = System.Int32.TryParse(tbWei4.Text, out Wei4);
                ifWei5 = System.Int32.TryParse(tbWei5.Text, out Wei5);
                ifWei6 = System.Int32.TryParse(tbWei6.Text, out Wei6);
                ifWeiSpecial = System.Int32.TryParse(tbWeiSpecial.Text, out WeiSpecial);

                if((ifWei1==true)&& (ifWei2 == true) && (ifWei3 == true) && (ifWei4 == true) && 
                   (ifWei5 == true) && (ifWei6 == true) && (ifWeiSpecial == true))
                {
                    //驗證主號碼是否為1~38，特別號為1~8
                    if((Wei1>0)&&(Wei1<=38)&& (Wei2 > 0) && (Wei2 <=38) && (Wei3 > 0) && (Wei3 <=38) &&
                       (Wei4 > 0) && (Wei4 <=38) && (Wei5 > 0) && (Wei5 <= 38) && (Wei6 > 0) && (Wei6 <= 38)
                       && (WeiSpecial > 0) && (WeiSpecial <= 8))
                    {
                        
                        //清空選號myWeilist，並重新抓取選號資料輸入myWeilist
                        myWeiList.RemoveRange(0, myWeiList.Count);
                        myWeiList.Add(WeiSpecial);
                        myWeiList.Add(Wei1);
                        myWeiList.Add(Wei2);
                        myWeiList.Add(Wei3);
                        myWeiList.Add(Wei4);
                        myWeiList.Add(Wei5);
                        myWeiList.Add(Wei6);
                        

                        //驗證主號碼是否有重複
                        bool ifDuplicate = false;
                        for (int i=1;i<6;i+=1)
                        {
                            for(int j=i+1;j<=6;j+=1)
                            {
                                if(myWeiList[i]==myWeiList[j])
                                {
                                    ifDuplicate = true;
                                }
                            }
                        }

                        if(ifDuplicate==false)
                        {
                            //對獎
                            //先核對主號碼
                            int myMatch = 0;
                            for (int i = 1; i <= 6; i += 1)
                            {
                                for (int j = 1; j <= 6; j += 1)
                                {
                                    if (myWeiList[j] == myWinWeiList[i])
                                    {
                                        myMatch += 1;
                                    }
                                }

                            }

                            if (myWeiList[0] == myWinWeiList[0])
                            {//特別號有中獎
                                switch (myMatch)
                                {
                                    case 1:
                                        lblWeiMessage.Text = "恭喜您中了普獎";
                                        break;
                                    case 2:
                                        lblWeiMessage.Text = "恭喜您中了捌獎";
                                        break;
                                    case 3:
                                        lblWeiMessage.Text = "恭喜您中了柒獎";
                                        break;
                                    case 4:
                                        lblWeiMessage.Text = "恭喜您中了伍獎";
                                        break;
                                    case 5:
                                        lblWeiMessage.Text = "恭喜您中了參獎";
                                        break;
                                    case 6:
                                        lblWeiMessage.Text = "恭喜您中了頭獎";
                                        break;
                                    default:
                                        lblWeiMessage.Text = "銘謝惠顧，祝您下次中大獎";
                                        break;
                                }


                            }
                            else
                            {//特別號沒中獎
                                switch (myMatch)
                                {
                                    case 3:
                                        lblWeiMessage.Text = "恭喜您中了玖獎";
                                        break;
                                    case 4:
                                        lblWeiMessage.Text = "恭喜您中了陸獎";
                                        break;
                                    case 5:
                                        lblWeiMessage.Text = "恭喜您中了肆獎";
                                        break;
                                    case 6:
                                        lblWeiMessage.Text = "恭喜您中了貳獎";
                                        break;
                                    default:
                                        lblWeiMessage.Text = "銘謝惠顧，祝您下次中大獎";
                                        break;
                                }
                            }
                        }
                        else
                        {
                            lblWeiMessage.Text = "號碼不可重複，請重新輸入";
                        }

                    }
                    else
                    {
                        lblWeiMessage.Text = "主號碼必須為1~38之間，特別號必須介於1~8之間";
                    }

                }
                else
                {
                    lblWeiMessage.Text = "主號碼必須為1~38之間，特別號必須介於1~8之間";
                }
            }
            else
            {
                lblWeiMessage.Text = "請輸入您的號碼，或按下電腦選號";
            }
        }

        //威力彩電腦選號
        private void btnWeiComputer_Click(object sender, EventArgs e)
        {
            //若已經有電腦選號，則先清除選取號碼
            if (myWeiList.Count > 0)
            {
                myWeiList.Clear();

            }

            //產生號碼池
            List<int> myNumPool = new List<int>();
            for (int i = 0; i < 38; i += 1)
            {
                myNumPool.Add(i + 1);
            }

            Random myRand = new Random();


            //特別號先選號
            int indexWeiSpecial = myRand.Next(8);//產生特別號碼隨機數
            tbWeiSpecial.Text=(myNumPool[indexWeiSpecial]).ToString();//特別號tb呈現選號
            myWeiList.Add(myNumPool[indexWeiSpecial]);//寫進選號myWeiList

            //主號碼選號
            for (int i = 0; i < 6; i += 1) 
            {
                int indexWinMain = myRand.Next(myNumPool.Count);//產生主號碼隨機數
                myWeiList.Add(myNumPool[indexWinMain]);//寫進選號myWeiList
                myNumPool.RemoveAt(indexWinMain);//號碼池刪除已選取的號碼，避免選號重複
            }

            //將主號碼進行排序
            myWeiList.Sort(1, 6, null);
            
            //主號碼輸出到tb
            tbWei1.Text = myWeiList[1].ToString();
            tbWei2.Text = myWeiList[2].ToString();
            tbWei3.Text = myWeiList[3].ToString();
            tbWei4.Text = myWeiList[4].ToString();
            tbWei5.Text = myWeiList[5].ToString();
            tbWei6.Text = myWeiList[6].ToString();
        }

        //威力彩清空選號資料
        private void btnWeiClearChoose_Click(object sender, EventArgs e)
        {
            //清空選號資料
            tbWei1.Text = "";
            tbWei2.Text = "";
            tbWei3.Text = "";
            tbWei4.Text = "";
            tbWei5.Text = "";
            tbWei6.Text = "";
            tbWeiSpecial.Text = "";
        }


        //***********************************今彩539*******************************************

        //今彩539開獎
        private void btnJingWinNumber_Click(object sender, EventArgs e)
        {
            if (myWinJingList.Count > 0)
            {
                myWinJingList.Clear();
            }

            //產生號碼池
            List<int> myNumPool = new List<int>();
            for (int i = 0; i < 39; i += 1)
            {
                myNumPool.Add(i + 1);
            }

            Random myRand = new Random();


            //開獎
            for (int i = 0; i < 5; i += 1)
            {
                int indexWin = myRand.Next(myNumPool.Count);//產生號碼隨機數
                myWinJingList.Add(myNumPool[indexWin]);//寫進開獎號碼myWinJingList
                myNumPool.RemoveAt(indexWin);//號碼池刪除已開獎號碼，避免開獎號碼重複
            }


            myWinJingList.Sort(0, 5, null);//將開獎號碼進行排序

            //從magelist將開獎號碼圖片抓出來輸出
            pbJing1.Image = imagelistNum.Images[myWinJingList[0] - 1];
            pbJing2.Image = imagelistNum.Images[myWinJingList[1] - 1];
            pbJing3.Image = imagelistNum.Images[myWinJingList[2] - 1];
            pbJing4.Image = imagelistNum.Images[myWinJingList[3] - 1];
            pbJing5.Image = imagelistNum.Images[myWinJingList[4] - 1];

            //啟用對獎按鈕
            btnJingMatch.Enabled = true;

        }

        //今彩539對獎
        private void btnJingMatch_Click(object sender, EventArgs e)
        {
            if ((tbJing1.Text.Length > 0) && (tbJing2.Text.Length > 0) && (tbJing3.Text.Length > 0) &&
               (tbJing4.Text.Length > 0) && (tbJing5.Text.Length > 0))//驗證是否有輸入號碼
            {
                //驗證輸入的號碼格式是否正確(非其他文字、小數...)
                bool ifJing1 = false;
                bool ifJing2 = false;
                bool ifJing3 = false;
                bool ifJing4 = false;
                bool ifJing5 = false;


                int Jing1 = 0, Jing2 = 0, Jing3 = 0, Jing4 = 0, Jing5 = 0;


                ifJing1 = System.Int32.TryParse(tbJing1.Text, out Jing1);
                ifJing2 = System.Int32.TryParse(tbJing2.Text, out Jing2);
                ifJing3 = System.Int32.TryParse(tbJing3.Text, out Jing3);
                ifJing4 = System.Int32.TryParse(tbJing4.Text, out Jing4);
                ifJing5 = System.Int32.TryParse(tbJing5.Text, out Jing5);

                if ((ifJing1 == true) && (ifJing2 == true) && (ifJing3 == true) && (ifJing4 == true) &&
                   (ifJing5 == true))
                {
                    //驗證號碼是否為1~39
                    if ((Jing1 > 0) && (Jing1 <= 39) && (Jing2 > 0) && (Jing2 <= 39) && (Jing3 > 0) && (Jing3 <= 39) &&
                       (Jing4 > 0) && (Jing4 <= 39) && (Jing5 > 0) && (Jing5 <= 39))
                    {

                        //清空選號myJinglist，並重新抓取選號資料輸入myJinglist
                        myJingList.Clear();
                        myJingList.Add(Jing1);
                        myJingList.Add(Jing2);
                        myJingList.Add(Jing3);
                        myJingList.Add(Jing4);
                        myJingList.Add(Jing5);


                        //驗證號碼是否有重複
                        bool ifDuplicate = false;
                        for (int i = 0; i < 4; i += 1)
                        {
                            for (int j = i + 1; j < 5; j += 1)
                            {
                                if (myJingList[i] == myJingList[j])
                                {
                                    ifDuplicate = true;
                                }
                            }
                        }

                        if (ifDuplicate == false)
                        {
                            //對獎
                            int myMatch = 0;
                            for (int i = 0; i <5 ; i += 1)
                            {
                                for (int j = 0; j <5; j += 1)
                                {
                                    if (myJingList[j] == myWinJingList[i])
                                    {
                                        myMatch += 1;
                                    }
                                }

                            }

                                switch (myMatch)
                                {
                                    case 2:
                                        MessageBox.Show("恭喜您中了肆獎");
                                        break;
                                    case 3:
                                        MessageBox.Show("恭喜您中了參獎");
                                        break;
                                    case 4:
                                        MessageBox.Show("恭喜您中了貳獎");
                                        break;
                                    case 5:
                                        MessageBox.Show("恭喜您中了頭獎");
                                        break;
                                    default:
                                        MessageBox.Show("銘謝惠顧，祝您下次中大獎");
                                        break;
                                }

                            
                        }
                        else
                        {
                            MessageBox.Show("號碼不可重複，請重新輸入", "錯誤訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else
                    {
                        MessageBox.Show("號碼必須為1~39之間", "錯誤訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    MessageBox.Show("號碼必須為1~39之間", "錯誤訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("請輸入您的號碼，或按下電腦選號","錯誤訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //今彩539電腦選號
        private void btnJingComputer_Click(object sender, EventArgs e)
        {
            //若已經有電腦選號，則先清除選取號碼
            if (myJingList.Count > 0)
            {
                myJingList.Clear();

            }

            //產生號碼池
            List<int> myNumPool = new List<int>();
            for (int i = 0; i < 39; i += 1)
            {
                myNumPool.Add(i + 1);
            }

            Random myRand = new Random();

            //選號
            for (int i = 0; i < 5; i += 1)
            {
                int indexWin = myRand.Next(myNumPool.Count);//產生主號碼隨機數
                myJingList.Add(myNumPool[indexWin]);//寫進選號myJingList
                myNumPool.RemoveAt(indexWin);//號碼池刪除已選取的號碼，避免選號重複
            }

            //將主號碼進行排序
            myJingList.Sort(0, 5, null);

            //主號碼輸出到tb
            tbJing1.Text = myJingList[0].ToString();
            tbJing2.Text = myJingList[1].ToString();
            tbJing3.Text = myJingList[2].ToString();
            tbJing4.Text = myJingList[3].ToString();
            tbJing5.Text = myJingList[4].ToString();

        }


        //今彩539清空選號資料
        private void btnJinClear_Click(object sender, EventArgs e)
        {
            //清空選號資料
            tbJing1.Text = "";
            tbJing2.Text = "";
            tbJing3.Text = "";
            tbJing4.Text = "";
            tbJing5.Text = "";

        }

        //***********************************四星彩*******************************************

        //四星彩開獎
        private void btnFourWinNumber_Click(object sender, EventArgs e)
        {
            //若已經有開獎，則先清除開獎號碼
            if (myWinFourList.Count > 0)
            {
                myWinFourList.Clear();
            }

            //開獎
            Random myRand = new Random();

            int myWinNumber = myRand.Next(10000);

            //將開獎號碼四個數字分別取出
            int myWinThousand = myWinNumber / 1000;
            int myWinHundred = (myWinNumber - (myWinThousand * 1000))/100;
            int myWinTen = (myWinNumber - (myWinThousand * 1000 + myWinHundred * 100)) / 10;
            int myWinOne = (myWinNumber - (myWinThousand * 1000 + myWinHundred * 100 + myWinTen * 10));


            //將四個數字存到開獎號碼List
            myWinFourList.Add(myWinThousand);
            myWinFourList.Add(myWinHundred);
            myWinFourList.Add(myWinTen);
            myWinFourList.Add(myWinOne);

            //取出開獎號碼圖片
            pbFour1.Image = imageListStarNum.Images[myWinFourList[0]];
            pbFour2.Image = imageListStarNum.Images[myWinFourList[1]];
            pbFour3.Image = imageListStarNum.Images[myWinFourList[2]];
            pbFour4.Image = imageListStarNum.Images[myWinFourList[3]];

            //啟用對獎按鈕
            btnFourMatch.Enabled = true;
             
        }

        private void chkFourNormal_CheckedChanged(object sender, EventArgs e)
        {//若正彩打勾，則組彩打勾消掉
            if(chkFourNormal.Checked == true)
            {
                chkFourGroup.Checked = false;
            }

        }

        private void chkFourGroup_CheckedChanged(object sender, EventArgs e)
        {//若組彩打勾，則正彩打勾消掉
            if (chkFourGroup.Checked == true)
            {
                chkFourNormal.Checked = false;
            }
            
        }

        private void btnFourMatch_Click(object sender, EventArgs e)
        {
            string strMsg = "";

            if ((tbFour1.Text.Length > 0) && (tbFour2.Text.Length > 0) && (tbFour3.Text.Length > 0) &&
            (tbFour4.Text.Length > 0))//驗證是否有輸入號碼
            {
                //驗證輸入的號碼格式是否正確(非其他文字、小數...)
                bool ifFour1 = false;
                bool ifFour2 = false;
                bool ifFour3 = false;
                bool ifFour4 = false;

                int Four1 = 0, Four2 = 0, Four3 = 0, Four4 = 0;


                ifFour1 = System.Int32.TryParse(tbFour1.Text, out Four1);
                ifFour2 = System.Int32.TryParse(tbFour2.Text, out Four2);
                ifFour3 = System.Int32.TryParse(tbFour3.Text, out Four3);
                ifFour4 = System.Int32.TryParse(tbFour4.Text, out Four4);

                if ((ifFour1 == true) && (ifFour2 == true) && (ifFour3 == true) && (ifFour4 == true))
                {
                    //驗證號碼是否為0~9
                    if ((Four1 >= 0) && (Four1 <= 9) && (Four2 >= 0) && (Four2 <= 9) && (Four3 >= 0) && (Four3 <= 9) &&
                       (Four4 >= 0) && (Four4 <= 9))
                    {
                        //清空選號myFourlist，並重新抓取選號資料輸入myFourlist
                        myFourList.Clear();
                        myFourList.Add(Four1);
                        myFourList.Add(Four2);
                        myFourList.Add(Four3);
                        myFourList.Add(Four4);


                        if (chkFourNormal.Checked==true)
                        {
                            //正彩對獎
                            int myMatch = 0;
                            for (int i = 0; i < 4; i += 1)
                            {
                                if(myFourList[i]==myWinFourList[i])
                                {
                                    myMatch += 1;
                                }
                            }
                            

                            switch (myMatch)
                            {
                                case 4:
                                    MessageBox.Show("恭喜您中了正彩");
                                    break;
                                default:
                                    MessageBox.Show("再接再厲，祝您下次中大獎");
                                    break;
                            }


                        }
                        else if(chkFourGroup.Checked==true)
                        {   
                            //驗證組彩號碼四個數字是否相同
                            if ((Four1 == Four2) && (Four1 == Four3) && (Four1 == Four4))
                            {
                                MessageBox.Show("組彩號碼四個數字不可相同", "錯誤訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {

                                //組彩獎項類別決定
                                int myNum = 0;
                                for (int i = 0; i < 4; i += 1)
                                {
                                    for (int j = 0; j < 4; j += 1)
                                    {
                                        if (i == j)
                                        {

                                        }
                                        else
                                        {
                                            if (myWinFourList[i] == myWinFourList[j])
                                            {
                                                myNum += 1;
                                            }
                                        }
                                    }
                                }

                                switch (myNum)
                                {
                                    case 0:
                                        strMsg = "24組彩";
                                        break;
                                    case 2:
                                        strMsg = "12組彩";
                                        break;
                                    case 4:
                                        strMsg = "6組彩";
                                        break;
                                    case 6:
                                        strMsg = "4組彩";
                                        break;
                                    default:
                                        break;
                                }

                                List<int> mytmpWinFourList = new List<int>();
                                mytmpWinFourList = myWinFourList.ToList();

                                myFourList.Sort();
                                mytmpWinFourList.Sort();

                                //組彩對獎
                                int myMatch = 0;
                                for (int i = 0; i < 4; i += 1)
                                {
                                    if (myFourList[i] == mytmpWinFourList[i])
                                    {
                                        myMatch += 1;
                                    }
                                }


                                switch (myMatch)
                                {
                                    case 4:
                                        MessageBox.Show(string.Format("恭喜您中了{0}", strMsg));
                                        break;
                                    default:
                                        MessageBox.Show("再接再厲，祝您下次中大獎");
                                        break;
                                }

                            }
                        }
                        else
                        {
                            MessageBox.Show("請選擇您的投注方式", "錯誤訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else
                    {
                        MessageBox.Show("號碼必須為0~9之間", "錯誤訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    MessageBox.Show("號碼必須為0~9之間", "錯誤訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("請輸入您的號碼，或按下電腦選號", "錯誤訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }

        private void btnFourComputer_Click(object sender, EventArgs e)
        {
            //電腦選號
            Random myRand = new Random();

            int myWinNumber = myRand.Next(10000);

            //將電腦選號四個數字分別取出
            int myWinThousand = myWinNumber / 1000;
            int myWinHundred = (myWinNumber - (myWinThousand * 1000)) / 100;
            int myWinTen = (myWinNumber - (myWinThousand * 1000 + myWinHundred * 100)) / 10;
            int myWinOne = (myWinNumber - (myWinThousand * 1000 + myWinHundred * 100 + myWinTen * 10));

            //將選號結果輸出到tb
            tbFour1.Text = myWinThousand.ToString();
            tbFour2.Text = myWinHundred.ToString();
            tbFour3.Text = myWinTen.ToString();
            tbFour4.Text = myWinOne.ToString();


        }

        private void btnFourClear_Click(object sender, EventArgs e)
        {
            
            //清空選號
            tbFour1.Text = "";
            tbFour2.Text = "";
            tbFour3.Text = "";
            tbFour4.Text = "";
            chkFourGroup.Checked = false;
            chkFourNormal.Checked = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
