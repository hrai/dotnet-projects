import { Component, Inject } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { NgForm } from '@angular/forms';
import { DecimalPipe } from '@angular/common';

@Component({
    selector: 'paymentdetails',
    templateUrl: './paymentdetails.component.html'
})
export class PaymentDetailsComponent {
    public paymentDetails: PaymentDetails;
    submitted: boolean;

    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) {
        this.paymentDetails = new PaymentDetails();
        this.submitted = false;
    }

    displayMessage(status: number, paymentsForm: NgForm) {
        if (status === 200) {
            alert('Payment successfully completed.');
            paymentsForm.reset();
        } else {
            alert('Error encountered. Please contact support.');
        }
    }

    onSubmit(paymentsForm: NgForm) {
        this.submitted = true;

        if (!paymentsForm.valid) {
            console.log("Form is invalid.");
            return;
        } else {
            console.log("Submitting form....");

            let headers = new Headers({ 'Content-Type': 'application/json' });
            let body = JSON.stringify(this.paymentDetails);

            this.http.post(this.baseUrl + 'api/Payment/SubmitPayments',
                body,
                { headers: headers }
            ).subscribe(response => this.displayMessage(response.status, paymentsForm));
        }
    }
}

class PaymentDetails {
    bsb: number;
    accountNumber: number;
    accountName: string;
    reference: string;
    paymentAmount: number;
}
