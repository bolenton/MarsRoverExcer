import { TestBed } from '@angular/core/testing';
import { NasaService } from './nasa.service';
describe('NasaService', function () {
    beforeEach(function () { return TestBed.configureTestingModule({}); });
    it('should be created', function () {
        var service = TestBed.get(NasaService);
        expect(service).toBeTruthy();
    });
});
//# sourceMappingURL=nasa.service.spec.js.map