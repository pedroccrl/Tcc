import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CidadeMapaComponent } from './cidade-mapa.component';

describe('CidadeMapaComponent', () => {
  let component: CidadeMapaComponent;
  let fixture: ComponentFixture<CidadeMapaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CidadeMapaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CidadeMapaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
