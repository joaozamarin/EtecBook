import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registroForm!: FormGroup;

  constructor(
    private fb: FormBuilder
  ) { }

  ngOnInit() {
    this.registroForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', Validators.compose([Validators.required, Validators.email])],
      password: ['', Validators.compose([Validators.required, Validators.minLength(6)])]
    });
  }

  checkName(): boolean {
    return this.registroForm.controls['name'].dirty && this.registroForm.controls['name'].errors?.['required'];
  }

  checkEmail(): boolean {
    return this.registroForm.controls['email'].dirty && this.registroForm.controls['email'].errors?.['required'];
  }

  checkEmailValid(): boolean {
    return this.registroForm.controls['email'].dirty && this.registroForm.controls['email'].errors?.['email'] && this.registroForm.controls['email'].touched;
  }

  checkPassword(): boolean {
    return this.registroForm.controls['password'].dirty && this.registroForm.controls['password'].errors?.['required'];
  }

  checkPasswordLength(): boolean {
    return this.registroForm.controls['password'].dirty && this.registroForm.controls['password'].errors?.['minlength'] && this.registroForm.controls['password'].touched;
  }

  onSubmit() {
    if (this.registroForm.valid) {
      // Enviar os dados ao backend
      console.log(this.registroForm.value);
    } else {
      // Dispara erro
      this.validateAllFormFields(this.registroForm);
    }
  }

  private validateAllFormFields(formGroup: FormGroup) {
    Object.keys(formGroup.controls).forEach(field => {
      const control = formGroup.get(field);

      if (control instanceof FormControl) {
        control.markAsDirty({ onlySelf: true })
      } else if (control instanceof FormGroup) {
        this.validateAllFormFields(control);
      }
    })
  }
}
