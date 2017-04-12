module.exports = function(grunt) {
	
	function ass(name) {

		var projectRoot = 'src/' + name + '/';
		
		// Load information about the assembly
		var assembly = grunt.file.readJSON(projectRoot + 'Properties/AssemblyInfo.json');

		// Get the version of the package
		var version = assembly.informationalVersion ? assembly.informationalVersion : assembly.version;
		
		return {
			name: name,
			projectRoot: projectRoot,
			assembly: assembly,
			description: assembly.description,
			version: version
		};
	
	}

	var path = require('path');

	// Load the package JSON file
	var pkg = grunt.file.readJSON('package.json');

	// get the root path of the project
	var projectRoot = 'src/' + pkg.name + '/';

	// Declare projects/packages
	var gridData = ass(pkg.name);
	var leBlender = ass(pkg.name + '.LeBlender');
	
	grunt.initConfig({
		pkg: pkg,
		clean: {
			files: [
				'releases/temp/'
			]
		},
		copy: {
			gridData: {
				files: [
					{
						expand: true,
						cwd: gridData.projectRoot + 'bin/Release/',
						src: [
							'Skybrud.Essentials.dll',
							'Skybrud.Essentials.xml',
							pkg.name + '.dll',
							pkg.name + '.xml'
						],
						dest: 'releases/temp/GridData/bin/'
					}
				]
			},
			leBlender: {
				files: [
					{
						expand: true,
						cwd: leBlender.projectRoot + 'bin/Release/',
						src: [
							'Lecoati.LeBlender.Extension.dll',
							'Lecoati.LeBlender.Extension.xml',
							'Skybrud.Essentials.dll',
							'Skybrud.Essentials.xml',
							pkg.name + '.dll',
							pkg.name + '.xml',
							pkg.name + '.LeBlender.dll',
							pkg.name + '.LeBlender.xml'
						],
						dest: 'releases/temp/LeBlender/bin/'
					}
				]
			}
		},
		zip: {
			gridData: {
				cwd: 'releases/temp/GridData/',
				src: [
					'releases/temp/GridData/**/*.*'
				],
				dest: 'releases/GridData/github/' + gridData.name + '.v' + gridData.version + '.zip'
			},
			leBlender: {
				cwd: 'releases/temp/LeBlender/',
				src: [
					'releases/temp/LeBlender/**/*.*'
				],
				dest: 'releases/LeBlender/github/' + leBlender.name + '.v' + leBlender.version + '.zip'
			}
		},
		umbracoPackage: {
			gridData: {
				src: 'releases/temp/GridData/',
				dest: 'releases/GridData/umbraco',
				options: {
					name: gridData.name,
					version: gridData.version,
					url: pkg.url,
					license: pkg.license.name,
					licenseUrl: pkg.license.url,
					author: pkg.author.name,
					authorUrl: pkg.author.url,
					readme: gridData.readme,
					outputName: gridData.name + '.v' + gridData.version + '.zip'
				}
			},
			leBlender: {
				src: 'releases/temp/LeBlender/',
				dest: 'releases/LeBlender/umbraco',
				options: {
					name: leBlender.name,
					version: leBlender.version,
					url: pkg.url,
					license: pkg.license.name,
					licenseUrl: pkg.license.url,
					author: pkg.author.name,
					authorUrl: pkg.author.url,
					readme: leBlender.readme,
					outputName: leBlender.name + '.v' + leBlender.version + '.zip'
				}
			}
		},
		nugetpack: {
			gridData: {
				src: 'src/' + gridData.name + '/' + gridData.name + '.csproj',
				dest: 'releases/GridData/nuget/'
			},
			leBlender: {
				src: 'src/' + leBlender.name + '/' + leBlender.name + '.csproj',
				dest: 'releases/LeBlender/nuget/'
			}
		}
	});

	grunt.loadNpmTasks('grunt-umbraco-package');
	grunt.loadNpmTasks('grunt-contrib-clean');
	grunt.loadNpmTasks('grunt-contrib-copy');
	grunt.loadNpmTasks('grunt-nuget');
	grunt.loadNpmTasks('grunt-zip');

	grunt.registerTask('release', ['clean', 'copy:gridData', 'zip:gridData', 'umbracoPackage:gridData', 'nugetpack:gridData', 'clean']);
	grunt.registerTask('gridData', ['clean', 'copy:gridData', 'zip:gridData', 'umbracoPackage:gridData', 'nugetpack:gridData', 'clean']);
	grunt.registerTask('leBlender', ['clean', 'copy:leBlender', 'zip:leBlender', 'umbracoPackage:leBlender', 'nugetpack:leBlender', 'clean']);
	grunt.registerTask('all', ['clean', 'copy', 'zip', 'umbracoPackage', 'nugetpack', 'clean']);

	grunt.registerTask('default', ['release']);

};