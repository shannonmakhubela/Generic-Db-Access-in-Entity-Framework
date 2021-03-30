using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenDemo
{
    public partial class frmLecture : Form
    {
        //instatiate datalayer class.
        service<Lecture> dl = new service<Lecture>();
        //global items
        int gid = -1;
       
        public frmLecture()
        {
            InitializeComponent();
        }
        Lecture createLecture()
        {
            //create Lecture.
            Lecture lec = new Lecture()
            {
                Name = txt_name.Text.Trim(),
                Surname = txt_surname.Text.Trim(),
                Email = txt_email.Text.Trim(),
                Password = txt_password.Text.Trim(),
                Active = "1"
            };
            return lec;
        }

        private void btnAddLecture_Click(object sender, EventArgs e)
        {

            try
            {
                //create Lecture.
                var lec = createLecture();

                // save to Database.
                var status = dl.AddItem(lec);
                if (status)
                {
                    MessageBox.Show(" Lecture Added Successfully");
                }
                else
                {
                    MessageBox.Show(" Failed to add Lecture");
                }

            }
            catch(Exception ex)
            {
                ex.GetBaseException();
            }

        }

        private void btnUpdateLecture_Click(object sender, EventArgs e)
        {
            try
            {
                // get lecture from the ID
                var st = dl.FetchItem(i => i.Id.Equals(gid));
                
                if (st !=  null)
                {
                    // create Lecture from the textboxes
                    var lec = createLecture();
                    // assign new Lecture  to the old lecture.
                    st.Name = lec.Name;
                    st.Surname = lec.Surname;
                    st.Email = lec.Email;
                    st.Password = lec.Password;

                    // the submit.
                    var status = dl.UpdateItem(st);
                    if (status)
                    {
                        MessageBox.Show(" Lecture updated!");
                        read();
                    }
                    else
                    {
                        MessageBox.Show(" Failed to Update Lecture ");
                    }


                }


            }
            catch (Exception ex)
            {
                ex.GetBaseException();
                MessageBox.Show(" Failed to get Lecture ");
            }
        }

        private void btnDeleteLecture_Click(object sender, EventArgs e)
        {
            try
            {

                var lec = dl.FetchItem(i => i.Id.Equals(gid));

                if (lec != null)
                {
                    var st = dl.DeleteItem(lec);
                    if (st)
                    {
                        MessageBox.Show("Lecture succesfully deleted.");
                        read();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete Lecture");
                    }
                }



            }
            catch (Exception ex)
            {
                ex.GetBaseException();
                MessageBox.Show("Failed : Something must be wrong with the inputs");
            }
        }

        private void listLecture_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gid = int.Parse(listLecture.SelectedItems[0].SubItems[0].Text);
                txt_name.Text = listLecture.SelectedItems[0].SubItems[1].Text;
                txt_surname.Text = listLecture.SelectedItems[0].SubItems[2].Text;
                txt_email.Text = listLecture.SelectedItems[0].SubItems[3].Text;
                txt_password.Text = listLecture.SelectedItems[0].SubItems[4].Text;


            }
            catch(Exception ex)
            {
                ex.GetBaseException();
                MessageBox.Show("No Item Selected ");
                clear();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //read from the list View.
        void read()
        {
            try
            {
                //clean list before display.
                if (listLecture != null)
                {
                    listLecture.Items.Clear();
                }

                //fetch list items.
                var lc = dl.ListItems();
                if (lc != null)
                {
                    foreach (var i in lc)
                    {
                        ListViewItem list = new ListViewItem(i.Id.ToString() , 0);
                        list.SubItems.Add(i.Name);
                        list.SubItems.Add(i.Surname);
                        list.SubItems.Add(i.Email);
                        list.SubItems.Add(i.Password);
                        //  list.SubItems.Add(i.Active);

                        //add attem sto the listlecture
                        listLecture.Items.Add(list);
                    }
                    
                }
                else
                {
                    MessageBox.Show(" The are not Lctures, create Some");
                }
            }
            catch(Exception ex)
            {
                ex.GetBaseException();
                MessageBox.Show(" Something went wrong");
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            //read.
            read();
        }
        void clear()
        {
            gid = -1;
            txt_name.Text = "";
            txt_surname.Text = "";
            txt_email.Text = "";
            txt_password.Text = "";
        }
    }
}
