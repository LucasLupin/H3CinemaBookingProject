import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
// import { GenericService } from 'src/app/services/generic.services';
import { UserService } from 'src/app/services/generic.services';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm!: FormGroup;
  error: string = '';
  constructor(private formBuilder: FormBuilder, private userService: UserService, private router: Router) {}

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      username: [''], // Initialiser feltet
      password: [''], // Initialiser feltet
    });
  }

  onLogin() {
    // Kontroller, om loginForm indeholder værdier, før indsendelse
    if (this.loginForm.value) {
      const { username, password } = this.loginForm.value;
      this.userService.login(username, password).subscribe({
        next: (success) => {
          if (success) {
            this.router.navigate(['/loggedinsuccess']);
          } else {
            this.error = 'Invalid credentials';
          }
        },
        error: (err) => {
          this.error = err.message;
        }
      });
    }
  }

  showSignupDesign(): void {
    // const showSignUpSection = document.getElementById('show-signup-section');
    const signUpSection = document.getElementById('signup-section');
    const signinSection = document.getElementById('signin-section');
    // if (showSignUpSection && signUpSection && signinSection) {
    if (signUpSection && signinSection) {
      if (signUpSection.classList.contains('show')) {
        signUpSection.classList.remove('show');
        // showSignUpSection.classList.add('show');
        signinSection.classList.add('show');
        return;
      }
      else {
        signUpSection.classList.add('show');
        // showSignUpSection.classList.remove('show');
        signinSection.classList.remove('show');
        return;
      }
    }
  }

  onInputChange(inputField: EventTarget | null) {
    if (inputField instanceof HTMLInputElement) {
      if (inputField.name === 'password') {
        const valid = this.checkPasswordStrength(inputField.value);
        if (valid) {
          inputField.style.borderColor = 'green';
        } else {
          inputField.style.borderColor = 'red';
        }
      }
    } else {
      console.error('Ugyldig inputfelt.');
    }
  }

  onInputFocus(inputField: EventTarget | null) {
    if (inputField instanceof HTMLInputElement) {
      console.log('Input felt ID:', inputField.name);
      if (inputField.name === 'password') {
        console.log('Password input field focused');
        const passwordReq = document.getElementById('password-requirements');
        if (passwordReq) {
          passwordReq.classList.add('show');
        }
      }
    } else {
      console.error('Ugyldig inputfelt.');
    }
  }

  onInputBlur(inputField: EventTarget | null) {
    if (inputField instanceof HTMLInputElement) {
      if (inputField.name === 'password') {
        console.log('Password input field blurred');
        const passwordReq = document.getElementById('password-requirements');
        if (passwordReq) {
          passwordReq.classList.remove('show');
        }
      }
    } else {
      console.error('Ugyldig inputfelt.');
    }
  }

  checkPasswordStrength(password: string): boolean {
    const minLength = 8;
    const hasUppercase = /[A-Z]/.test(password || '');
    const hasLowercase = /[a-z]/.test(password || '');
    const hasNumber = /\d/.test(password || '');
    const hasSpecialChar = /[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]+/.test(password || '');
  
    const passLengthSpan = document.getElementById('pass-length-span');
    const passDigitSpan = document.getElementById('pass-digit-span');
    const passUppercaseSpan = document.getElementById('pass-uppercase-span');
    const passLowercaseSpan = document.getElementById('pass-lowercase-span');
    const passSymbolSpan = document.getElementById('pass-symbol-span');
    let cnt = 0;
  
    if (password && password.length >= minLength) {
      if (passLengthSpan) {
        passLengthSpan.innerText = 'Yes';
        cnt++;
      }
    }
    else {
      if (passLengthSpan) {
        passLengthSpan.innerText = 'No';
      }
    }
  
    if (hasUppercase) {
      if (passUppercaseSpan) {
        passUppercaseSpan.innerText = 'Yes';
        cnt++;
      }
    }
    else {
      if (passUppercaseSpan) {
        passUppercaseSpan.innerText = 'No';
      }
    }
  
    if (hasLowercase) {
      if (passLowercaseSpan) {
        passLowercaseSpan.innerText = 'Yes';
        cnt++;
      }
    }
    else {
      if (passLowercaseSpan) {
        passLowercaseSpan.innerText = 'No';
      }
    }
  
    if (hasNumber) {
      if(passDigitSpan) {
        passDigitSpan.innerText = 'Yes';
        cnt++;
      }
    }
    else {
      if (passDigitSpan) {
        passDigitSpan.innerText = 'No';
      }
    }
  
    if (hasSpecialChar) {
      if (passSymbolSpan) {
        passSymbolSpan.innerText = 'Yes';
        cnt++;
      }
    }
    else {
      if (passSymbolSpan) {
        passSymbolSpan.innerText = 'No';
      }
    }
  
    console.log('cnt:', cnt);
    return cnt === 5;
  }

}
