import { ChangeDetectorRef, Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageType } from 'app/core/models/layouts/message-type';
import { CustomerSize } from 'app/core/models/customer-size';
import { MessageService } from 'app/core/services/layouts/message.service';
import { CustomerService } from 'app/core/services/customer.service';
import { BaseFormGroupComponent } from 'app/shared/components/base-form-group.component';

@Component({
  selector: 'app-customer-edit',
  templateUrl: './customer-edit.component.html',
  styleUrls: ['./customer-edit.component.scss']
})
export class CustomerEditComponent extends BaseFormGroupComponent<any> {

  public customerSize = CustomerSize;

  constructor(
    fb: FormBuilder,
    private router: Router,
    changeDetector: ChangeDetectorRef,
    messageService: MessageService,
    private customerService: CustomerService,
    private activeRoute: ActivatedRoute) {
    super(fb, changeDetector, messageService);
    this.model = {};
  }

  public ngOnInit(): void {
    super.ngOnInit();

    this.activeRoute.paramMap.subscribe(params => {
      this.load(+params.get('id'));
    });
  }

  public load(id: number): void {
    if (id) {
      this.customerService.getById(id).subscribe(data => {
        this.showFormValue(data, { includesModel: true });
      });
    }
    else {
      this.showFormValue({}, { includesModel: true });
    }
  }

  public formChanged(oldForm: FormGroup, newForm: FormGroup): void {
    super.formChanged(oldForm, newForm);

    newForm.setControl('id', this.fb.control({ value: null, disabled: true }));
    newForm.setControl('name', this.fb.control(null));
    newForm.setControl('size', this.fb.control(null));
  }

  public save() {
    const customer: any = {};

    if (this.tryGetFormValue(customer, { includesModel: true })) {

      if (customer.id > 0) {
        this.customerService.update(customer).subscribe(data => {
          this.showFormValue(data, { includesModel: true });

          this.messageService.message.open('Cliente salvo com sucesso.', MessageType.Success, true)
            .finally(this.navigateTo.bind(this));
        });
      } else {

        this.customerService.create(customer).subscribe(data => {
          this.showFormValue(data, { includesModel: true });

          this.messageService.message.open('Cliente salvo com sucesso.', MessageType.Success, true)
            .finally(this.navigateTo.bind(this));
        });
      }
    }
  }

  private navigateTo(): any {
    this.router.navigate(['/customers']);
  }

}

