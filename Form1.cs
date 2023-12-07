using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ExchangeOffice
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void RateReplace()
        {
            txtRate.Text = txtRate.Text.Replace(".", ",");
        }

        private void DataClear()
        {
            if (txtRate.Text.Length > 0)
            {
                txtRate.Clear();
                txtPrice.Clear();
                txtQuantity.Clear();
                txtRemaining.Clear();
            }
        }

        private void GetRateData()
        {
            string today = "https://tcmb.gov.tr/kurlar/today.xml";
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(today);

            string dollarBuy = xmlDocument.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteBuying").InnerXml;
            lblDollarBuy.Text = dollarBuy;
            string dollarSale = xmlDocument.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteSelling").InnerXml;
            lblDollarSale.Text = dollarSale;

            string euroBuy = xmlDocument.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteBuying").InnerXml;
            lblEuroBuy.Text = euroBuy;
            string euroSale = xmlDocument.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteSelling").InnerXml;
            lblEuroSale.Text = euroSale;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetRateData();
        }

        private void btnDollarBuy_Click(object sender, EventArgs e)
        {
            if (txtRate.Text.Length > 0)
            {
                txtRate.Clear();
                txtRate.Text = lblDollarBuy.Text;
            }
            else
            {
                txtRate.Text = lblDollarBuy.Text;
            }
            RateReplace();
        }

        private void btnDollarSale_Click(object sender, EventArgs e)
        {
            if (txtRate.Text.Length > 0)
            {
                txtRate.Clear();
                txtRate.Text = lblDollarSale.Text;
            }
            else
            {
                txtRate.Text = lblDollarSale.Text;
            }
            RateReplace();
        }

        private void btnEuroBuy_Click(object sender, EventArgs e)
        {
            if (txtRate.Text.Length > 0)
            {
                txtRate.Clear();
                txtRate.Text = lblEuroBuy.Text;
            }
            else
            {
                txtRate.Text = lblEuroBuy.Text;
            }
            RateReplace();
        }

        private void btnEuroSale_Click(object sender, EventArgs e)
        {
            if (txtRate.Text.Length > 0)
            {
                txtRate.Clear();
                txtRate.Text = lblEuroSale.Text;
            }
            else
            {
                txtRate.Text = lblEuroSale.Text;
            }
            RateReplace();
        }


        private void btnTLUSD_Click(object sender, EventArgs e)
        {
            try
            {
                double rate = Convert.ToDouble(txtRate.Text);
                int quantity = Convert.ToInt32(txtQuantity.Text);
                int price = Convert.ToInt32(quantity / rate);
                txtPrice.Text = price.ToString("0.00");
                double remaining;
                remaining = quantity % rate;
                txtRemaining.Text = remaining.ToString("0.00");


                double numberFromTLLabel;
                double numberFromUSDLabel;
                double numberFromPriceTextBox;

                if (string.IsNullOrEmpty(txtRemaining.Text))
                {
                    txtRemaining.Text = "0";
                }

                if (double.TryParse(lblTL.Text, out numberFromTLLabel))
                {
                    double resultTL = numberFromTLLabel + Convert.ToDouble(txtQuantity.Text) - Convert.ToDouble(txtRemaining.Text);
                    lblTL.Text = resultTL.ToString("0.00");
                }

                if (double.TryParse(lblUSD.Text, out numberFromUSDLabel) &&
                    double.TryParse(txtPrice.Text, out numberFromPriceTextBox))
                {
                    double resultUSD = numberFromUSDLabel - numberFromPriceTextBox; 
                    lblUSD.Text = resultUSD.ToString("0.00");
                }
                MessageBox.Show("Kasa Bilgileri Güncellendi");
                DataClear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("İşlem Sırasında Bir Hata Oluştu." + ex.Message);
            }
        }

        private void btnTLEUR_Click(object sender, EventArgs e)
        {
            try
            {
                double rate = Convert.ToDouble(txtRate.Text);
                int quantity = Convert.ToInt32(txtQuantity.Text);
                int price = Convert.ToInt32(quantity / rate);
                txtPrice.Text = price.ToString("0.00");
                double remaining;
                remaining = quantity % rate;
                txtRemaining.Text = remaining.ToString("0.00");

                double numberFromTLLabel;
                double numberFromEURLabel;
                double numberFromPriceTextBox;
                
                if (string.IsNullOrEmpty(txtRemaining.Text))
                {
                    txtRemaining.Text = "0";
                }

                if (double.TryParse(lblTL.Text, out numberFromTLLabel))
                {
                    double resultTL = numberFromTLLabel + Convert.ToDouble(txtQuantity.Text) - Convert.ToDouble(txtRemaining.Text);
                    lblTL.Text = resultTL.ToString("0.00");
                }

                if (double.TryParse(lblEUR.Text, out numberFromEURLabel) &&
                    double.TryParse(txtPrice.Text, out numberFromPriceTextBox))
                {
                    double resultEUR = numberFromEURLabel - numberFromPriceTextBox;
                    lblEUR.Text = resultEUR.ToString("0.00");
                }
                MessageBox.Show("Kasa Bilgileri Güncellendi");
                DataClear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("İşlem Sırasında Bir Hata Oluştu." + ex.Message);
            }
        }

        private void btnEURTL_Click(object sender, EventArgs e)
        {
            try
            {
                double rate, quantity, price;
                rate = Convert.ToDouble(txtRate.Text);
                quantity = Convert.ToDouble(txtQuantity.Text);
                price = rate * quantity;
                txtPrice.Text = price.ToString("0.00");

                double numberFromTLLabel;
                double numberFromEURLabel;
                double numberFromPriceTextBox;

                if (string.IsNullOrEmpty(txtRemaining.Text))
                {
                    txtRemaining.Text = "0";
                }

                if (double.TryParse(lblTL.Text, out numberFromTLLabel))
                {
                    double resultTL = numberFromTLLabel - Convert.ToDouble(txtPrice.Text) - Convert.ToDouble(txtRemaining.Text);
                    lblTL.Text = resultTL.ToString("0.00");
                }

                if (double.TryParse(lblEUR.Text, out numberFromEURLabel) &&
                    double.TryParse(txtQuantity.Text, out numberFromPriceTextBox))
                {
                    double resultEUR = numberFromEURLabel + numberFromPriceTextBox;
                    lblEUR.Text = resultEUR.ToString("0.00");
                }
                MessageBox.Show("Kasa Bilgileri Güncellendi");
                DataClear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("İşlem Sırasında Bir Hata Oluştu." + ex.Message);
            }
        }

        private void btnUSDTL_Click(object sender, EventArgs e)
        {
            try
            {
                double rate, quantity, price;
                rate = Convert.ToDouble(txtRate.Text);
                quantity = Convert.ToDouble(txtQuantity.Text);
                price = rate * quantity;
                txtPrice.Text = price.ToString("0.00");

                double numberFromTLLabel;
                double numberFromUSDLabel;
                double numberFromPriceTextBox;

                if (string.IsNullOrEmpty(txtRemaining.Text))
                {
                    txtRemaining.Text = "0";
                }

                if (double.TryParse(lblTL.Text, out numberFromTLLabel))
                {
                    double resultTL = numberFromTLLabel - Convert.ToDouble(txtPrice.Text) - Convert.ToDouble(txtRemaining.Text);
                    lblTL.Text = resultTL.ToString("0.00");
                }

                if (double.TryParse(lblUSD.Text, out numberFromUSDLabel) &&
                    double.TryParse(txtQuantity.Text, out numberFromPriceTextBox))
                {
                    double resultUSD = numberFromUSDLabel + numberFromPriceTextBox;
                    lblUSD.Text = resultUSD.ToString("0.00");
                }
                MessageBox.Show("Kasa Bilgileri Güncellendi");
                DataClear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("İşlem Sırasında Bir Hata Oluştu." + ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            int number = 0;

            lblUSD.Text = number.ToString();
            lblEUR.Text = number.ToString();
            lblTL.Text = number.ToString();
        }

        private void BtnGetRate_Click(object sender, EventArgs e)
        {
            GetRateData();
        }
    }
}

