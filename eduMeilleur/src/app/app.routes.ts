import { Routes } from '@angular/router';
import { SignUpComponent } from './sign-up/sign-up.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { ProfileComponent } from './profile/profile.component';
import { AiComponent } from './ai/ai.component';

export const routes: Routes = [
    {path: "", redirectTo: "/home", pathMatch: "full"},
    {path: "home", component: HomeComponent},
    {path: "signup", component: SignUpComponent},
    {path: "login", component: LoginComponent},
    {path: "profile", component: ProfileComponent},
    {path: "ai", component: AiComponent}
];
