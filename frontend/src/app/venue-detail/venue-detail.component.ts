import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatListModule } from '@angular/material/list';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-venue-detail',
  templateUrl: './venue-detail.component.html',
  styleUrls: ['./venue-detail.component.css'],
  imports: [CommonModule, MatCardModule,
    MatProgressSpinnerModule,
    MatListModule,
    MatButtonModule, MatFormFieldModule, ReactiveFormsModule, MatInputModule ]
})
export class VenueDetailComponent implements OnInit {
  venueId: number = 0;
  venue: Venue | null = null;
  events: Event[] = [];
  eventForm: FormGroup;
  isLoading = true;
  isSubmitting = false;
  error: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private http: HttpClient,
    private fb: FormBuilder
  ) {
    this.eventForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(100)]],
      date: ['', Validators.required],
      description: ['', Validators.maxLength(500)],
      organizer: ['', Validators.maxLength(100)],
      ticketPrice: [null, Validators.min(0)]
    });
  }

  ngOnInit(): void {
    this.venueId = Number(this.route.snapshot.paramMap.get('id'));
    if (this.venueId) {
      this.fetchVenueDetails(this.venueId);
      this.fetchEvents(this.venueId);
    } else {
      this.error = 'Invalid venue ID';
      this.isLoading = false;
    }
  }

  fetchVenueDetails(id: number): void {
    this.http.get<Venue>(`https://localhost:7035/api/Venue/${id}`).subscribe({
      next: (data) => {
        this.venue = data;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error fetching venue details:', err);
        this.error = 'Could not fetch venue details. Please try again later.';
        this.isLoading = false;
      }
    });
  }

  fetchEvents(venueId: number): void {
    this.http.get<Event[]>(`https://localhost:7035/api/Event/venue/${venueId}`).subscribe({
      next: (data) => {
        this.events = data;
      },
      error: (err) => {
        console.error('Error fetching events:', err);
        this.error = 'Could not fetch events. Please try again later.';
      }
    });
  }

  submitEvent(): void {
    if (this.eventForm.valid && this.venueId) {
      this.isSubmitting = true;
      const eventData = {
        ...this.eventForm.value,
        venueID: this.venueId,
        date: new Date(this.eventForm.value.date) 
      };

      this.http.post(`https://localhost:7035/api/Event`, eventData).subscribe({
        next: (response) => {
          console.log('Event successfully created:', response);
          this.eventForm.reset();
          this.fetchEvents(this.venueId);
          this.isSubmitting = false;
        },
        error: (err) => {
          console.error('Error creating event:', err);
          this.isSubmitting = false;
        }
      });
    }
  }
}

export interface Venue {
  venueID: number;
  name: string;
  address: string;
  city: string;
  state: string;
  capacity: number;
  contactNumber?: string;
  email?: string;
}

export interface Event {
  eventID: number;
  name: string;
  date: string;
  venueID: number;
  description?: string;
  organizer?: string;
  ticketPrice?: number;
}
