import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-venue-form',
  templateUrl: './venue-form.component.html',
  styleUrls: ['./venue-form.component.css'],
  imports: [ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSnackBarModule, CommonModule]

})
export class VenueFormComponent {
  venueForm: FormGroup;
  isSubmitting = false;

  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router) {
    this.venueForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(100)]],
      address: ['', [Validators.required, Validators.maxLength(200)]],
      city: ['', [Validators.required, Validators.maxLength(50)]],
      state: ['', [Validators.required, Validators.maxLength(50)]],
      capacity: [null, [Validators.required, Validators.min(1)]],
      contactNumber: ['', [Validators.required]], 
      email: ['', [Validators.email]] 
    });
  }

  submitForm(): void {
    if (this.venueForm.valid) {
      this.isSubmitting = true;
      const venueData = this.venueForm.value;

      this.http.post('https://localhost:7035/api/Venue', venueData).subscribe({
        next: (response) => {
          console.log('Venue successfully created:', response);
          this.isSubmitting = false;
          this.venueForm.reset();
          this.router.navigate(['/']);
        },
        error: (error) => {
          console.error('Error creating venue:', error);
          this.isSubmitting = false;
        }
      });
    }
  }
}
