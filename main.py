from subprocess import run as run_command

from templateframework.runner import run
from templateframework.template import Template
from templateframework.metadata import Metadata


class ContainerEnvCsTemplate(Template):
    def pre_hook(self, metadata: Metadata) -> Metadata:
        return metadata

    def post_hook(self, metadata: Metadata):
        print('\nInstall dependencies ...')

        args = ['dotnet', 'restore', 'src']
        run_command(args,
                    capture_output=False,
                    shell=False,
                    cwd=metadata.target_path,
                    check=True)


if __name__ == '__main__':
    run(ContainerEnvCsTemplate())
