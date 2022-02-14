using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Constructs;
using StackSpot.Env.Container;
using StackSpot.Env.Vpc;

namespace {{ computed_inputs.app_class_name }}
{
    public class {{ computed_inputs.app_class_name }}Stack : Stack
    {
        internal {{ computed_inputs.app_class_name }}Stack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            {% if inputs.container_has_vpc %}
            string[] subnetsIds = {
                {% set subnet_ids = inputs.container_subnet_ids.replace(' ', '').split(',') %}
                {% for subnet_id in subnet_ids %}
                "{{ subnet_id }}",
                {% endfor %}
            };

            {% endif %}
            VpcEnvComponent vpcEnvComponent = new VpcEnvComponent(this, "{{ computed_inputs.app_class_name }}Vpc", new VpcEnvComponentProps
            {
                {% if inputs.container_has_vpc %}
                SubnetsIds = subnetsIds,
                SubnetsType = SubnetType.{{ inputs.container_subnet_type }},
                VpcDefault = {% if inputs.vpc_default %}true{% else %}false{% endif %},
                {% if not inputs.vpc_default %}
                VpcId = "{{ inputs.container_vpc_id }}",
                {% endif %}
                VpcRegion = "{{ inputs.vpc_region }}",
                {% else %}
                VpcCidr = "{{ inputs.container_vpc_cidr }}",
                VpcMaxAzs = {{ inputs.vpc_max_azs }},
                VpcName = "{{ inputs.vpc_name }}",
                {% endif %}
            });

            ContainerEnvComponent environment = new ContainerEnvComponent(this, "{{ computed_inputs.app_class_name }}Container", new ContainerEnvComponentProps
            {
                ClusterName = "{{ computed_inputs.app_class_name }}Cluster",
                ContainerInsights = {% if inputs.container_insights %}true{% else %}false{% endif %},
                Subnets = vpcEnvComponent.Subnets,
                Vpc = vpcEnvComponent.Vpc
            });
        }
    }
}
