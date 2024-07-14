import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DynamicTreeView1Component } from './dynamic-tree-view-1.component';

describe('DynamicTreeView1Component', () => {
  let component: DynamicTreeView1Component;
  let fixture: ComponentFixture<DynamicTreeView1Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DynamicTreeView1Component]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DynamicTreeView1Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
