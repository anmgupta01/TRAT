import { AbstractControl, ValidatorFn } from "@angular/forms";

export function dateLessThan(dateField1: string, dateField2: string): ValidatorFn {
        return (form: AbstractControl): { [key: string]: boolean } | null => {
            const dateField1Value = form.get(dateField1)?.value;
            const dateField2Value = form.get(dateField2)?.value;

            if(!dateField1Value || !dateField2Value)
            {
                return {missing:true};
            }
            const firstDate = new Date(dateField1Value);
            const secondDate = new Date(dateField2Value);

            if(firstDate.getTime() > secondDate.getTime())
            {
                const err = {dateLessThan: true};
                form.get(dateField2)?.setErrors(err);
                return err;
            }
            return null;
        };
    }