import { AfterViewInit, Component, signal } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { SpinnerService } from '../../services/spinner.service';
import { ModalComponent } from '../../components/modal/modal.component';
import { Modal } from 'bootstrap';
import { SignUpMainComponent } from '../../components/sign-up-main/sign-up-main.component';
import { SignUpExtraComponent } from '../../components/sign-up-extra/sign-up-extra.component';
import { passwordsMatch } from '../../validators/passwords-match';
import { ModalService } from '../../services/modal.service';
import { ToastService } from '../../services/toast.service';

@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterLink, SignUpMainComponent, SignUpExtraComponent, ReactiveFormsModule],
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.css',
})
export class SignUpComponent implements AfterViewInit {
  errors: { [key: string]: string } = {};

  isFirstPage = signal(true);
  progressPercent = signal(0);

  formGroupMain: FormGroup;
  formGroupExtra: FormGroup;

  constructor(
    public userService: UserService,
    public route: Router,
    public spinner: SpinnerService,
    public formBuilder: FormBuilder,
    public modalService: ModalService,
    public toastService: ToastService,
  ) {
    this.formGroupMain = formBuilder.group(
      {
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required, Validators.minLength(5), Validators.pattern('(?=.*\\d).*')]],
        confirmPassword: ['', [Validators.required]],
      },
      { validators: passwordsMatch() },
    );

    this.formGroupExtra = formBuilder.group({
      username: ['', [Validators.required, Validators.pattern(/^[a-zA-Z0-9\-._@+/ ]+$/)]],
      school: ['', []],
      schoolYear: ['', []],
    });
  }

  ngAfterViewInit(): void {
    requestAnimationFrame(() => {
      this.progressPercent.set(50);
    });
  }

  goToSecondPage(): void {
    this.isFirstPage.set(false);
    this.progressPercent.set(100);
  }

  goToFirstPage(): void {
    this.isFirstPage.set(true);
    this.progressPercent.set(50);
  }

  async register() {
    this.spinner.show();
    this.errors = {};

    try {
      let email = this.formGroupMain.get('email')?.value;
      let password = this.formGroupMain.get('password')?.value;
      let username = this.formGroupExtra.get('username')?.value;
      let school = this.formGroupExtra.get('school')?.value;
      let schoolYear = this.formGroupExtra.get('schoolYear')?.value;

      await this.userService.register(username, email, password, parseFloat(school), parseFloat(schoolYear));

      if (localStorage.getItem('token') != null) {
        this.spinner.hide();
        this.route.navigate(['/profile']);
      }
    } catch (error: any) {
      if (error.error) {
        if (error.status === 0 || error.status === 500) {
          this.modalService.openErrorModal(() => this.register());
          this.spinner.hide();
          return;
        } else {
          this.toastService.error(error.error[0].description);
        }
      }
    } finally {
      this.spinner.hide();
    }
  }
}
