import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GamingMoodComponent } from './gaming-mood.component';

describe('GamingMoodComponent', () => {
  let component: GamingMoodComponent;
  let fixture: ComponentFixture<GamingMoodComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GamingMoodComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GamingMoodComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
