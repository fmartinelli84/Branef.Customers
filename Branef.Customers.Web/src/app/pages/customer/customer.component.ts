import { ChangeDetectorRef, Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MessageType } from 'app/core/models/layouts/message-type';
import { CustomerSize } from 'app/core/models/customer-size';
import { MessageService } from 'app/core/services/layouts/message.service';
import { CustomerService } from 'app/core/services/customer.service';
import { BaseModelComponent } from 'app/shared/components/base-model.component';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss']
})
export class CustomerComponent extends BaseModelComponent<any[]> {

  public customerSize = CustomerSize;

  constructor(
    changeDetector: ChangeDetectorRef,
    private messageService: MessageService,
    private customerService: CustomerService,
    private activeRoute: ActivatedRoute) {
    super(changeDetector);
  }

  public ngOnInit(): void {
    super.ngOnInit();

    this.activeRoute.url.subscribe(params => {
      this.load();
    });
  }

  public load(): void {
    this.customerService.getAll().subscribe(data => {
      this.model = data;
    });
  }

  public delete(id: number): void {

    this.customerService.delete(id).subscribe(data => {

      this.customerService.getAll().subscribe(data => {
        this.model = data;

        this.messageService.message.open('Cliente excluído com sucesso.', MessageType.Success, true);
      });
    });
  }
}
