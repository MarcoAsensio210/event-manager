import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-venue-screen',
  templateUrl: './venue-screen.component.html',
  styleUrls: ['./venue-screen.component.css'],
  imports: [
    MatTableModule,
    MatProgressSpinnerModule,
    CommonModule,
    MatIconModule,
    RouterModule
  ]
})
export class VenueScreenComponent implements OnInit {
  displayedColumns: string[] = ['venueID', 'name', 'address', 'city', 'state', 'capacity', 'contactNumber', 'email', 'actions'];
  dataSource = new MatTableDataSource<Venue>();
  isLoading = true;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.fetchVenues();
  }

  fetchVenues(): void {
    this.http.get<Venue[]>('https://localhost:7035/api/Venue')
      .subscribe({
        next: (data) => {
          this.dataSource.data = data;
          this.isLoading = false;
        },
        error: (error) => {
          console.error('Error fetching venues:', error);
          this.isLoading = false;
        }
      });
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
