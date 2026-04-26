import { ValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';

export function passwordsMatch(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const password = control.get('password');
    const confirm = control.get('confirmPassword');

    if (!password || !confirm) {
      return null;
    }

    const isMatch: boolean = password.value === confirm.value;

    if (!isMatch) {
      password.setErrors({ ...password.errors, passwordsMatch: true });
      confirm.setErrors({ ...confirm.errors, passwordsMatch: true });
      return { passwordsMatch: true };
    } else {
      const clearError = (control: AbstractControl) => {
        if (!control.errors) return;
        const { passwordsMatch, ...others } = control.errors;
        control.setErrors(Object.keys(others).length ? others : null);
      };

      clearError(password);
      clearError(confirm);

      return null;
    }
  };
}