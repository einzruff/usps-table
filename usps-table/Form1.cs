/* USPS-TABLE
 * Aggregate up-to-date postage prices into a table
 * by Daphne Lundquist
 * August 2, 2018
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace usps_table
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string urlAddress = txtURL.Text;
            string datafound = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }
                datafound = readStream.ReadToEnd();
                response.Close();
                readStream.Close();
            }
            string letter1oz = "";
            string letter2oz = "";
            string letter3oz = "";
            string letter35oz = "";
            string large1oz = "";
            string large2oz = "";
            string large3oz = "";
            string large4oz = "";
            string large5oz = "";
            string large6oz = "";
            string large7oz = "";
            string large8oz = "";
            string large9oz = "";
            string large10oz = "";
            string large11oz = "";
            string large12oz = "";
            string large13oz = "";
            //US!
            //_c037 contains  Letters (Stamped)  Large Envelopes   Postcard
            //_c038 is the letters
            //_c107 containts  First-Class Package (Parcels) rates
            //CANADA!
            //_c341 contains  Canada First-Class  Postcards, Letters, Large Envelopes
            //<div id="_c341">
            //_c342 is just the postcards
            //_c343 is the letters
            //_c348 is the large envelopes
            ///////////////////////////////////////////////////////////////////////////////////////////////
            //Canada Postcard
            string canada = "<div id=\"_c342\">";
            int found342 = datafound.IndexOf(canada);
            //start at found341 and get the first <tbody>
            int tbody342 = datafound.IndexOf("<tbody>", found342);
            //start at tbody342 amd get the 2nd <td>
            int td342first = datafound.IndexOf("<td>", tbody342);
            int td342second = datafound.IndexOf("<td>", td342first+5);
            //get the position of the end </td>
            int td342endtd = datafound.IndexOf("</td>",td342second);
            //get string of the Canada postcard amount
            string td342outerstr = datafound.Substring(td342second, (td342endtd - td342second));
            int td342amt = td342outerstr.IndexOf("$");
            int td342amtend = td342outerstr.IndexOf("</p>");
            string canadapostcard = td342outerstr.Substring(td342amt, (td342amtend - td342amt));
            if (found342 != -1)
            {
                lvResults.Items.Add("Canada Postcard: " + canadapostcard);
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////
            //
        }
    }
}
