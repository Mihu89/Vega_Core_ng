import { Component, OnInit } from "@angular/core";
import { MakeService, FeatureService, VehicleService } from "../../services";
import { ToastyService } from "ng2-toasty";

@Component({
  selector: "app-vehicle-form",
  templateUrl: "./vehicle-form.component.html",
  styleUrls: ["./vehicle-form.component.css"],
})
export class VehicleFormComponent implements OnInit {
  makes: any[];
  models: any[];
  vehicle: any = {
    features: [],
    contact: {},
  };

  features: any[];
  constructor(
    private makeService: MakeService,
    private featureService: FeatureService,
    private vehicleService: VehicleService,
    private toastyService: ToastyService
  ) {}

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
    let selectedMake = this.makes.find((m) => m.id == this.vehicle.makeId);
    this.models = selectedMake ? selectedMake.models : [];
    delete this.vehicle.modelId;
  }

  onFeatureToggle(featureId, $event) {
    if ($event.target.checked) {
      this.vehicle.features.push(featureId);
    } else {
      let index = this.vehicle.features.indexOf(featureId);
      if (index > -1) {
        this.vehicle.features.splice(index, 1);
      }
    }
  }

  submit() {
    this.vehicleService.createVehicle(this.vehicle).subscribe(
      (x) => console.log(x),
      (err) => {
        this.toastyService.error({
          title: " Error on Saving Vehicle",
          msg: " An error happend on saving vehicle",
          theme: "bootsytrap",
          showClose: true,
          timeout: 5000,
        });
      }
    );
  }
}
