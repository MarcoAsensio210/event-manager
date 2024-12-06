import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VenueScreenComponent } from './venue-screen/venue-screen.component';
import { VenueFormComponent } from './venue-form/venue-form.component';
import { VenueDetailComponent } from './venue-detail/venue-detail.component';
import { VenueUpdateComponent } from './venue-update/venue-update.component';
import { EventFormComponent } from './event-form/event-form.component';
import { HttpClientModule } from '@angular/common/http';
import { MatTableModule } from '@angular/material/table';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';


export const routes: Routes = [
  { path: '', component: VenueScreenComponent },
  { path: 'venueForm', component: VenueFormComponent },
  { path: 'venueDetail/:id', component: VenueDetailComponent },
  { path: 'venueEdit/:id', component: VenueUpdateComponent },
  { path: 'eventForm', component: EventFormComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
