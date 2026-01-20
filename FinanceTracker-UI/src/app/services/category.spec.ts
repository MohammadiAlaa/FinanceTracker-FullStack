import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { CategoryService } from './category';

// تأكد من استيراد الاسم الصحيح وهو CategoryService

describe('CategoryService', () => {
  let service: CategoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      // ضيف دول عشان السيرفيس بتستخدم HttpClient ومطلعش ايرور تاني
      providers: [CategoryService, provideHttpClient(), provideHttpClientTesting()],
    });
    service = TestBed.inject(CategoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
