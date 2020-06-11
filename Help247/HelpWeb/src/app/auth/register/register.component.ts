import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CustomerRegisterModel, RegisterModel, HelperRegisterModel } from '../models/register.model';
import { AuthService } from '../services/auth.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { HelperService } from 'src/app/shared/services/helper.service';
import { PaginationBase } from 'src/app/shared/models/pagination-base.model';
import { HelperCategoryModel } from 'src/app/helper/models/helper-category.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  customerForm: FormGroup;
  helperForm: FormGroup;

  userType: number;
  customerModel: CustomerRegisterModel;
  registerModel: RegisterModel;
  helperModel: HelperRegisterModel;
  helperCategoryList: Array<HelperCategoryModel>
  pagination: PaginationBase;

  constructor(private helperService: HelperService, private route: ActivatedRoute, private fb: FormBuilder, private authService: AuthService) {
    this.pagination = new PaginationBase();
  }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.userType = +params.get('userType');
    });
    this.registerModel = JSON.parse(sessionStorage.getItem('user'));
    if (this.userType == 3) {
      this.initCustomerForm();
      this.customerForm.patchValue({
        email: this.registerModel.email
      });
    }
    else {
      this.getHelperCategory();
      this.initHelperForm();
      this.helperForm.patchValue({
        email: this.registerModel.email
      });
    }
  }

  initCustomerForm() {
    this.customerForm = this.fb.group({
      name: ['Test Customer', [Validators.required]],
      phoneNo: ['+94774065416', [Validators.required]],
      email: [null, [Validators.required]],
      addressLine: ['311/1/A Malay Colony Ambalantota', [Validators.required]],
      country: ['Sri Lanka', [Validators.required]],
      city: ['Ambalantota', [Validators.required]],
      state: ['Hambantota', [Validators.required]],
      postalCode: ['82100', [Validators.required]],
      profilePicUrl: ['http://www.google.com', [Validators.required]]
    });
  }

  initHelperForm() {
    this.helperForm = this.fb.group({
      firstName: ['Test', [Validators.required]],
      lastName: ['Customer', [Validators.required]],
      phoneNo: ['+94774065416', [Validators.required]],
      mobileNo: ['+94774065416', [Validators.required]],
      email: [null, [Validators.required]],
      addressLine: ['311/1/A Malay Colony Ambalantota', [Validators.required]],
      country: ['Sri Lanka', [Validators.required]],
      city: ['Ambalantota', [Validators.required]],
      state: ['Hambantota', [Validators.required]],
      postalCode: ['82100', [Validators.required]],
      profilePicUrl: ['http://www.google.com', [Validators.required]],
      experience: [2, [Validators.required]],
      aboutMe: ['I am Sri lankan', [Validators.required]],
      myService: ['About my service description', [Validators.required]],
      helperCategoryId: [null, [Validators.required]]
    });
  }

  getHelperCategory() {
    this.helperService.getHelperCategoryList(this.pagination).subscribe(
      result => {
        console.log('result', result);
        this.helperCategoryList = result.details;
      },
      error => {
        console.log('error', error);
      }
    );
  }

  onCustomerSubmit() {
    if (this.customerForm.invalid) {
      return;
    }
    this.customerModel = this.customerForm.value;
    this.customerModel.userName = this.registerModel.userName;
    this.customerModel.password = this.registerModel.password;
    this.customerModel.userType = this.userType;

    this.authService.register(this.customerModel).subscribe(
      result => {
        console.log('result', result);
      },
      error => {
        console.log('error', error);
      }
    );
  }

  onHelperSubmit() {
    if (this.helperForm.invalid) {
      return;
    }
    this.helperModel = this.helperForm.value;
    this.helperModel.userName = this.registerModel.userName;
    this.helperModel.password = this.registerModel.password;
    this.helperModel.userType = this.userType;

    // this.authService.register(this.helperModel).subscribe(
    //   result => {
    //     console.log('result', result);
    //   },
    //   error => {
    //     console.log('error', error);
    //   }
    // );
  }

}
