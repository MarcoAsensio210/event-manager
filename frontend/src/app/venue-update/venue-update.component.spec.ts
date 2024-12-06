import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VenueUpdateComponent } from './venue-update.component';

describe('VenueUpdateComponent', () => {
  let component: VenueUpdateComponent;
  let fixture: ComponentFixture<VenueUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VenueUpdateComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VenueUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
