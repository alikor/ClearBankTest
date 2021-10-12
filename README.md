# Documentation

The PaymentService MakePayment method was not functionary correct so i completed it

I would move the dataStoreType configuration to the host application Inversion of control container configuration which would be setup at startup.

I didn't really touch the PaymentScheme validation rules because i didn't fully understand them so i left them alone other than a little refactor.

I did not want to use a TransactionScope or any form of Two Phase commit for the updating of the debitor and creditor accounts as well as i didn't know how the AccountDataStore stores have been implemented and even if they support distributed transaction so i implemented a crude version of a Orchestration-based saga pattern using a finite state machine to manage the transaction and compensating actions if either update failed.

compensating actions reverting the balance to its previous value would have normally been done using something like Kafka or durable que to make sure that no transactions are lost because of catastrophic failure 