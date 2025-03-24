using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bank_account
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    class BankAccount
    {
        protected string name;
        protected int accountNum;
        protected double balance;

        //constructor, set all of the initial values
        public BankAccount(string lname, int laccountNum, double lbalance)
        {
            this.name = lname;
            this.accountNum = laccountNum;
            this.balance = lbalance;
        }

        public double getBalance()
        {
            return balance;
        }
        public void deposit(double amount)
        {
            balance += amount;
        }
        public virtual void withdraw(double amount)
        {
            balance -= amount;
        }
    }
}
