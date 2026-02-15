import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { UserService } from '../services/user.service';

export const authPagesGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const userService = inject(UserService);
  const token = userService.token();

  if (token) {
    return router.createUrlTree(['/'])
  }
  
  return true;
};
