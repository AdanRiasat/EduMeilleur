import { Routes } from '@angular/router';
import { SignUpComponent } from './sign-up/sign-up.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { ProfileComponent } from './profile/profile.component';
import { AiComponent } from './ai/ai.component';
import { SujetsComponent } from './sujets/sujets.component';
import { SujetComponent } from './sujet/sujet.component';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { EditProfileComponent } from './edit-profile/edit-profile.component';

export const routes: Routes = [
    {path: "", redirectTo: "/home", pathMatch: "full"},
    {path: "home", component: HomeComponent},
    {path: "signup", component: SignUpComponent},
    {path: "login", component: LoginComponent},
    {path: "profile", component: ProfileComponent},
    {path: "profile/:username", component: EditProfileComponent},
    {path: "ai", component: AiComponent},
    {path: "sujets", component: SujetsComponent},
    {path: "sujets/:id", component: SujetComponent},
    {path: "contactUs", component: ContactUsComponent}
];
