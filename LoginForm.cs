using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Data.SqlClient;

namespace login_form
{
//ENTITY FRAMEWORK
    public partial class Form1 : Form
    {
        detail detail = new detail();
        studentEntities7 db = new studentEntities7();
        public Form1()
        {
            InitializeComponent();
            warning.Visible = false;
            tabControl1.TabPages.Remove(tabPage2);
            tabControl1.TabPages.Remove(tabPage3);//TabPageControl
            tabControl1.TabPages.Remove(tabPage4);
       
        }
        private void btn_login_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_email.Text) && !string.IsNullOrEmpty(txt_password.Text))
                {
                    var v = db.details.ToList().Where(s => s.email == txt_email.Text).FirstOrDefault();
                    if (v==null)
                    {
                        MessageBox.Show("no data found");
                        return;
                    }
                    else
                    {
                        var check = (from s in db.details where s.email == txt_email.Text select s).First();//CheckEmailandPassword
                        if (check.password == txt_password.Text && check.email == txt_email.Text)
                        {
                            wrn_pwd.Visible = false;
                            tabControl1.TabPages.Remove(tabPage1);
                            tabControl1.TabPages.Add(tabPage2);
                            tabControl1.SelectedTab = tabPage2;
                        }
                        else
                        {
                            wrn_pwd.Visible = true;
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("fill all fields to login");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Enter valid email address", ex.Message);
            }
        }
        private void btn_signup_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabPage1);
            tabControl1.TabPages.Add(tabPage3);//SignUpButtonClick
            tabControl1.SelectedTab = tabPage3;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try//OrderTabPage
            {  
                var count = listBox1.Items.Count;     
                if (listBox1.Items.Count== 0)
                {
                    MessageBox.Show("place the order first");
                    return;
                }
                else
                {
                    int itemId = 0;
                    int order_Id = 0;
                    
                    if (db.orders.Count() > 0)
                    {
                        itemId = (from s in db.orders
                                             orderby s.item_id descending//ForIncreaseTheItemsCountInDatabaseTable
                                             select s.item_id).First();
                        order_Id =Convert.ToInt32( (from s in db.orders//ForIncreaseTheOrdersCountInDatabaseTable
                                    orderby s.orderId descending
                                    select s.orderId).FirstOrDefault());
                    }
                    for (int i = 0; i < count; i++)
                    {
                        order ord = new order();
                        ord.item = listBox1.Items[i].ToString();
                        ord.item_id = itemId + 1;
                        itemId = ord.item_id;
                        ord.orderId = order_Id + 1;
                        var a = (from s in db.logins where s.email == txt_email.Text select s.username).First();//SelectTheUserNameFromTheDatabase
                        ord.username = a;
                        db.orders.Add(ord);
                        db.SaveChanges();
                    }
                    MessageBox.Show("Order placed");
                    listBox1.Items.Clear();
                    return;
                    //EXIT BUTTON
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
                return;
            }
        }
        private void txt_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsLetter(ch) && ch != 8 && ch != 46 && ch != 32)//TextBoxCondition
            {
                e.Handled = true;
            }
        }

        private void txt_age_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)//TextBoxCondition
            {
                e.Handled = true;
            }
        }

        private void btn_register_Click(object sender, EventArgs e)
        {
            try//RegisterPage
            {
                detail detail1 = new detail();
                var condition = !string.IsNullOrEmpty(txt_regno1.Text) && !string.IsNullOrEmpty(txt_fname1.Text) && !string.IsNullOrEmpty(txt_lname1.Text) && !string.IsNullOrEmpty(txt_age1.Text) && !string.IsNullOrEmpty(txt_email1.Text) && !string.IsNullOrEmpty(txt_password1.Text) && !string.IsNullOrEmpty(txt_cnfrmpw1.Text);
                if (condition)
                {
                    int comp = Convert.ToInt32(txt_regno1.Text);
                    int count = db.details.ToList().Where(a=>a.regNo == comp).Count();
                    if (count > 0)
                    {
                        MessageBox.Show("already exists Id..try new one..");//CheckIDShouldNotRepated
                        return;
                    }
                    else
                    {
                        detail1.regNo = Convert.ToInt32(txt_regno1.Text);
                    }
                    var age = Convert.ToInt32(txt_age1.Text);
                    if (age <= 16)
                    {
                        warning.Visible = true;//AgeTextBoxCondition
                        MessageBox.Show("enter valid data");
                        return;
                    }
                    else
                    {
                        warning.Visible = false;
                        detail1.age = age;
                    }
                }
                else
                {
                    MessageBox.Show("* Please fill all the fields");
                    return;
                }
                detail1.fname = txt_fname1.Text;
                detail1.lname = txt_lname1.Text;//GettingAllDetails
                detail1.email = txt_email1.Text;
                if (txt_cnfrmpw1.Text == txt_password1.Text)
                {
                    detail1.password = txt_password1.Text;
                }
                else
                {
                    warning1.Visible = true;
                    return;
                }
                db.details.Add(detail1);
                db.SaveChanges();
                logintable();//FunctonCall
                MessageBox.Show("* Registered successfully");
                tabControl1.TabPages.Remove(tabPage3);
                tabControl1.TabPages.Add(tabPage1);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error");
            }
       }
        void logintable()
        {
            login loginDetail = new login();//SaveSameDetailsToLoginDetailsTable
            loginDetail.username = txt_fname1.Text + " "+txt_lname1.Text;
            loginDetail.email = txt_email1.Text;
            if (txt_cnfrmpw1.Text == txt_password1.Text)
            {
                loginDetail.password = txt_password1.Text;
            }
            else
            {
                warning1.Visible = true;
                return;
            }
            loginDetail.no = Convert.ToInt32(txt_regno1.Text);
            db.logins.Add(loginDetail);
            db.SaveChanges();
        }
        private void btn_resetpass_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabPage1);//TabPageControl
            tabControl1.TabPages.Add(tabPage4);
            tabControl1.SelectedTab = tabPage4;
        }
        private void txt_email1_Validating(object sender, CancelEventArgs e)
        {
            var condition = @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)\.([A-Za-z]{2,})$";//ConditionForEmailValidation
            System.Text.RegularExpressions.Regex email = new System.Text.RegularExpressions.Regex(condition);//+[a-zA-Z]
            if (!email.IsMatch(txt_email1.Text))                                                                // ^\w+[\w-\.]*\@\w+((-\w+)|(\w*))\.[a-z]{2,3}$   //^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
            {
                MessageBox.Show("enter a valid email address");
                return;
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            try//PasswordResetButton
            {
                if (!string.IsNullOrEmpty(email_reset.Text) && !string.IsNullOrEmpty(txt_pwd_new.Text) && !string.IsNullOrEmpty(txt_cnfrmpwd.Text))
                {
                    
                    var data = db.details.ToList().Where(a => a.email == email_reset.Text).FirstOrDefault();//FetchDetailsOfDatabaseTableDetails
                    if (data == null)
                    {
                        MessageBox.Show("no email found like this to reset");
                    }
                    else
                    {
                        if (txt_pwd_new.Text == txt_cnfrmpwd.Text)
                        {
                            data.password = txt_cnfrmpwd.Text;
                            db.details.Attach(data);//UpdateTheOldPasswordToNewPassword
                            login_table_pwd_rest();
                            db.Entry(data).State = EntityState.Modified;
                            
                            db.SaveChanges();
                            
                            MessageBox.Show("suuccess");
                            tabControl1.TabPages.Remove(tabPage2);
                            tabControl1.SelectedTab=tabPage1;
                           
                        }
                        else
                        {
                            MessageBox.Show("passwords are ot matched");
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("* Please fill all the fields");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                return;
            }
        }

        public void login_table_pwd_rest()
        {
            var data1 = db.logins.ToList().Where(b => b.email == email_reset.Text).FirstOrDefault();
            data1.password = txt_cnfrmpwd.Text;//SameResetInLoginTableALsoInTheDatabase
            db.logins.Attach(data1);
            db.Entry(data1).State = EntityState.Modified;
        }
        private void btn_item_1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(this.btn_item_1.Text);//OrderPageButtons
           return;
        }

        private void btn_item_2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(this.btn_item_2.Text);//OrderPageButtons
            return;
        }

        private void btn_item_3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(this.btn_item_3.Text);//OrderPageButtons
            return;
        }

        private void btn_item_4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(this.btn_item_4.Text);//OrderPageButtons
            return;
        }

        private void btn_item_5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(this.btn_item_5.Text);//OrderPageButtons
            return;
        }

        private void btn_item_6_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(this.btn_item_6.Text);//OrderPageButtons
            return;
        }

        private void btn_item_7_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(this.btn_item_7.Text);//OrderPageButtons
            return;
        }

        private void btn_item_8_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(this.btn_item_8.Text);//OrderPageButtons
            return;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();//CloseOrExit
        }
    }
}
