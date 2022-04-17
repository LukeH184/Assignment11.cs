using System;
using System.Threading;

class Program {
  public static void Main (string[] args) {
    CreditCard card = new CreditCard();
    CardHolder J = new CardHolder(card, "John");
    CardHolder M = new CardHolder(card, "Mary");
    Thread t1 = new Thread(J.Run);
    Thread t2 = new Thread(M.Run);
    t1.Start();
    t2.Start();
  }
  class CreditCard {
    private double balance;
    public CreditCard(){
      this.balance = 5000;
    }
    public double getBalance(){
      return this.balance;
    }
    public void withdraw(double x){
      this.balance -= x;
    }
  }
  class CardHolder{
    private static object padlock = new object();
    private CreditCard card;
    public string name;
    public CardHolder(CreditCard c, string s){
      this.card = c;
      this.name = s;
    }
    public void Run(){
      for (int i = 0; i < 6; i++){
          makeWithdraw(500);
          if (card.getBalance() < 0){
            Console.WriteLine("Not enough in account for \"name\"  to withdraw");
          }
      }
    }
    private void makeWithdraw(double d) {
      lock(padlock){
          if(card.getBalance() < d){
            Console.WriteLine("Error: Not enough in account for " + name + " to withdraw $" + d);
            //add the thread name later
          }
        else if (card.getBalance() >= d){
          try{
            Thread.Sleep(1000);
          }
          catch(ThreadInterruptedException tiex){
            Console.WriteLine("Error: " + tiex);
            }
          Console.WriteLine(name + " before withdrawing $" + d + ", balance: " + card.getBalance());
          card.withdraw(500);
          Console.WriteLine(name + " after withdrawing $" + d + ", balance: " + card.getBalance());
          
          }
        }
      }
    }
  }
