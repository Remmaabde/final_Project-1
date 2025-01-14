﻿using FinalProject.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class admin_page : Form
    {
        public admin_page()
        {
            InitializeComponent();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = AdminClass.GetAllProducts();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            errorProvider1.Clear();

            Regex r = new Regex(@"^([^0-9]*)$");
            Regex r2 = new Regex(@"[+][0-9]$");

            if (textBox4.Text.Length == 10)
            {

            }
            else
            {
                errorProvider1.SetError(textBox4, "Please enter 10 digits ");
            }

            if (string.IsNullOrEmpty(textBox2.Text))
            {
                errorProvider1.SetError(textBox2, " First Name is required ");
            }
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                errorProvider1.SetError(textBox3, " Last name is required ");
            }
            if (string.IsNullOrEmpty(textBox4.Text))
            {
                errorProvider1.SetError(textBox4, "Your Contact info is required");
            }
            if (string.IsNullOrEmpty(textBox7.Text))
            {
                errorProvider1.SetError(textBox7, "Your Email is required");
            }
            else if (!r.IsMatch(textBox2.Text))
                errorProvider1.SetError(textBox2, "Your Name should'nt contain numbers");

            
            else if (!r.IsMatch(textBox3.Text))
                errorProvider1.SetError(textBox3, "Your Name should'nt contain numbers");


            else
            {
                string gen;
                if (radioButton1.Checked)
                    gen = radioButton1.Text;
                else
                    gen = radioButton2.Text;

                try
                {
                    AdminClass ac = new AdminClass()
                    {
                        firstName = textBox2.Text,
                        lastName = textBox3.Text,
                        contactInfo = textBox4.Text,
                        DateOfBirth =dateTimePicker1.Value.ToString(),
                        Email = textBox7.Text,
                        Occupation = textBox8.Text,
                        Gender = gen
                    };


                    ac.save();
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource=AdminClass.GetAllProducts();
                    

                }
                catch (Exception)
                {
                    MessageBox.Show("Type mismatch");
                };

            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            var product = AdminClass.findOne(textBox9.Text);
            if (product == null)
            {
                MessageBox.Show("Employee doesn't Exist");
            }
            else
            {
                label9.Text = product.id.ToString();
                textBox2.Text = product.firstName;
                textBox3.Text = product.lastName;
                textBox4.Text = product.contactInfo;
                dateTimePicker1.Value = DateTime.Parse(product.DateOfBirth);
                textBox7.Text = product.Email;
                textBox8.Text = product.Occupation;
               /* if (product.Gender.ToString() == "female")
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;

                }*/

            }
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            string gen;
            if (radioButton1.Checked)
                gen = radioButton1.Text;
            else
                gen = radioButton2.Text;
            AdminClass.update(label9.Text, textBox2.Text, textBox3.Text, textBox4.Text,dateTimePicker1.Value.ToString(), textBox7.Text, textBox8.Text,gen);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = AdminClass.GetAllProducts();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AdminClass.delete(label9.Text);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = AdminClass.GetAllProducts();
        }

        private void admin_page_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
