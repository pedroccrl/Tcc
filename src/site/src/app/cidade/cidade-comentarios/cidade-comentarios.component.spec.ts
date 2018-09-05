import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CidadeComentariosComponent } from './cidade-comentarios.component';

describe('CidadeComentariosComponent', () => {
  let component: CidadeComentariosComponent;
  let fixture: ComponentFixture<CidadeComentariosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CidadeComentariosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CidadeComentariosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
