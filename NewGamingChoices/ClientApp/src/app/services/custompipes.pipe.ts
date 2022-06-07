import { Pipe, PipeTransform } from "@angular/core";

@Pipe({name: 'booleantotext'})
export class BooleanToTextPipe implements PipeTransform {
  transform(value: boolean) :string {
      if(value)
      { return "Oui"; }
      else
      { return "Non"; }
  }
}

@Pipe({name: 'discsize'})
export class DiscSizePipe implements PipeTransform {
  transform(value: number) : string {
      if(value / 1000 >= 1)
      { return (Math.round(value/100) / 10).toString() + " Go"; }
      else
      { return value.toString() + " Mo"; }
  }
}

@Pipe({name: 'requiredpowertext'})
export class RequiredPowerPipe implements PipeTransform {
  transform(value: number) : string {
    let requiredPowerValues = ["Rudimentaire", "Peu performant", "Standard", "Assez performant", "Excellent"];
    return requiredPowerValues[value - 1];
  }
}
