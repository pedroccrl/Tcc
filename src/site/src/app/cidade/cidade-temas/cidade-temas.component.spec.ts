import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CidadeTemasComponent } from './cidade-temas.component';

describe('CidadeTemasComponent', () => {
  let component: CidadeTemasComponent;
  let fixture: ComponentFixture<CidadeTemasComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CidadeTemasComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CidadeTemasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
