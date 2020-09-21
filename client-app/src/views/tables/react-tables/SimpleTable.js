import React from "react"
import { Card, CardHeader, CardTitle, CardBody } from "reactstrap"
import ReactTable from "react-table"
import { makeData } from "./TableData"

class SimpleTable extends React.Component {
  state = {
    data:[
      {
        first_name: "Roy",
        last_name: "Nybo",
        company_name: "Phoenix Phototype",
        address: "823 Fishers Ln",
        state: "ACT",
        post: 2603,
        city: "Red Hill",
        phone1: "02-5311-7778",
        phone2: "0416-394-795",
        email: "rnybo@nybo.net.au",
        web: "http://www.phoenixphototype.com.au"
      },
      {
        first_name: "Annamae",
        last_name: "Lothridge",
        company_name: "Highland Meadows Golf Club",
        address: "584 Meridian St #997",
        state: "ACT",
        post: 2608,
        city: "Civic Square",
        phone1: "02-1919-3941",
        phone2: "0495-759-817",
        email: "alothridge@hotmail.com",
        web: "http://www.highlandmeadowsgolfclub.com.au"
      },
      {
        first_name: "Katheryn",
        last_name: "Lamers",
        company_name: "Sonoco Products Co",
        address: "62171 E 6th Ave",
        state: "ACT",
        post: 2609,
        city: "Fyshwick",
        phone1: "02-4885-1611",
        phone2: "0497-455-126",
        email: "katheryn_lamers@gmail.com",
        web: "http://www.sonocoproductsco.com.au"
      },
      {
        first_name: "Jamie",
        last_name: "Kushnir",
        company_name: "Bell Electric Co",
        address: "3216 W Wabansia Ave",
        state: "ACT",
        post: 2901,
        city: "Tuggeranong Dc",
        phone1: "02-4623-8120",
        phone2: "0426-830-817",
        email: "jamie@kushnir.net.au",
        web: "http://www.bellelectricco.com.au"
      },
    ]
  }
  render() {
    const { data } = this.state

    return (
      <Card>
        <CardHeader>
          <CardTitle>Simple</CardTitle>
        </CardHeader>
        <CardBody>
          <ReactTable
            data={data}
            columns={[
              {
                Header: "Name",
                columns: [
                  {
                    Header: "First Name",
                    accessor: "firstName"
                  },
                  {
                    Header: "Last Name",
                    id: "lastName",
                    accessor: d => d.lastName
                  }
                ]
              },
              {
                Header: "Info",
                columns: [
                  {
                    Header: "Age",
                    accessor: "age"
                  },
                  {
                    Header: "Status",
                    accessor: "status"
                  }
                ]
              },
              {
                Header: "Stats",
                columns: [
                  {
                    Header: "Visits",
                    accessor: "visits"
                  }
                ]
              }
            ]}
            defaultPageSize={10}
            className="-striped -highlight"
          />
        </CardBody>
      </Card>
    )
  }
}
export default SimpleTable
