import React, { Component } from 'react';

class Processess extends Component {

    constructor(props) {
        super(props);
        this.state = {
            procs: []
        }
    }

    render() {
        return (
            <table className="table table-bordered table-borderless table-hover">
                <thead>
                    <tr>
                        <th scope="col">#</th>
                        <th scope="col">Processo</th>
                    </tr>
                </thead>
                <tbody>
                    {this.props.allData.map((p, index) =>
                        <tr key={p.id}>
                            <td>{index + 1}</td>
                            <td>{p.name}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        )
    }
}

export default Processess;