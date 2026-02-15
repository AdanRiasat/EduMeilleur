import { Routes } from '@angular/router';
import { SignUpComponent } from './pages/sign-up/sign-up.component';
import { LoginComponent } from './pages/login/login.component';
import { HomeComponent } from './pages/home/home.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { AiComponent } from './pages/ai/ai.component';
import { SujetsComponent } from './pages/sujets/sujets.component';
import { SujetComponent } from './pages/sujet/sujet.component';
import { ContactUsComponent } from './pages/contact-us/contact-us.component';
import { EditProfileComponent } from './pages/edit-profile/edit-profile.component';
import { authPagesGuard } from './guards/auth-pages.guard';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [
    {path: "", redirectTo: "/home", pathMatch: "full"},
    {path: "home", component: HomeComponent},
    {path: "signup", component: SignUpComponent, canActivate: [authPagesGuard]},
    {path: "login", component: LoginComponent, canActivate: [authPagesGuard]},
    {path: "profile", component: ProfileComponent, canActivate: [authGuard]},
    {path: "profile/:username", component: EditProfileComponent, canActivate: [authGuard]},
    {path: "ai", component: AiComponent},
    {path: "sujets", component: SujetsComponent},
    {path: "sujets/:id", component: SujetComponent},
    {path: "contactUs", component: ContactUsComponent}
];
