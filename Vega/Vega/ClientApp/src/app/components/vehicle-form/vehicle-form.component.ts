import { Component, OnInit } from '@angular/core';
import { MakeService, FeatureService, VehicleService} from '../../services';

@Component({
    selector: 'app-vehicle-form',
    templateUrl: './vehicle-form.component.html',
    styleUrls: ['./vehicle-form.component.css']
})

export class VehicleFormComponent implements OnInit {

    makes: any[];
    models: any[];
    vehicle: any = {};

    features: any[];
    constructor(private makeService: MakeService,
        private featureService: FeatureService,
        private vehicleService: VehicleService
    ) {
    }

    ngOnInit() {
        this.makeService.getMakes().subscribe((makes: any[]) => {
            this.makes = makes;
            // console.log("Makes: " + JSON.stringify(this.makes, null, 4));
        });

        this.featureService.getFeatures().subscribe((features: any[]) => {
            this.features = features;
        });
    }

    onMakeChange() {
        let selectedMake = this.makes.find(m => m.id == this.vehicle.make);
        this.models = selectedMake ? selectedMake.models : [];
    }

    createVehicle(){

    }

}