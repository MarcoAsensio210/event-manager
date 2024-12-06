import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VenueScreenComponent } from './venue-screen.component';

describe('VenueScreenComponent', () => {
  let component: VenueScreenComponent;
  let fixture: ComponentFixture<VenueScreenComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VenueScreenComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VenueScreenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
