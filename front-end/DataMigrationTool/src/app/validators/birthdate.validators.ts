import { AbstractControl } from '@angular/forms';


export function birthDateValidator(control: AbstractControl): {[s: string]: boolean} {
    let birthDate = control.value as Date;
    if (birthDate > new Date() || birthDate.getFullYear() < 1900) {
        return { 'birthDateIsInvalid': true };
    }
    return null;
}